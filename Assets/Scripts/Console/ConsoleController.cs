using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;

public delegate void CommandHandler(string[] args);

public class ConsoleController
{

    #region Event declarations
    //Used to communicate with ConsoleView
    public delegate void LogChangedHandler(string[] log);
    public event LogChangedHandler LogChanged;

    public delegate void VisibilityChangedHandler(bool visible);
    //public event VisibilityChangedHandler VisibilityChanged;
    #endregion

    //Object to hold information about each command
    class CommandRegistration
    {
        public string Command { get; private set; }
        public CommandHandler Handler { get; private set; }
        public string Help { get; private set; }

        public CommandRegistration(string command, CommandHandler handler, string help)
        {
            this.Command = command;
            this.Handler = handler;
            this.Help = help;
        }
    }

    //How many log lines should be retained?
    //Note that strings submitted to appendLogLine with embedded newlines will be counted as a single line.
    const int scrollbackSize = 16;

    Queue<string> scrollback = new Queue<string>(scrollbackSize);
    readonly List<string> commandHistory = new List<string>();
    readonly Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

    public string[] Log { get; private set; } //Copy of scrollback as an array for easier use by ConsoleView

    const string repeatCmdName = "repeat"; //Name of the repeat command, constant since it needs to skip these if they are in the command history

    bool echo = true;

    public ConsoleController()
    {
        //When adding commands, you must add a call below to registerCommand() with its name, implementation method, and help text.
        RegisterCommand("@echo", EchoSetting, "Echo given commands, can be set to on or off.");
        RegisterCommand("clear", Clear, "Clear log");
        RegisterCommand("echo", Echo, "Echoes arguments back as array (for testing argument parser)");
        RegisterCommand("experiance.add", ExperianceAdd, "Add experiance.");
        RegisterCommand("experiance.addPermanent", ExperianceAddPermanent, "Add permanent experiance.");
        RegisterCommand("fullScreen", FullScreen, "Ttoggle fullScree.n");
        RegisterCommand("help", Help, "Print this help.");
        RegisterCommand("kill", Kill, "Kills player. (can be given a delay)");
        RegisterCommand("reload", Reload, "Reload scene.");
        RegisterCommand(repeatCmdName, RepeatCommand, "Repeat last command.");
        RegisterCommand("sethp", PlayerHealth, "Set hitpoints. usage: sethp <hp> || sethp <hp> <maxhp>");
        RegisterCommand("tp", Teleport, "Teleport to X Y position. tp <x> <y>");
        RegisterCommand("tp.exit", TeleportToExit, "Teleport to end cell.");
        RegisterCommand("tp.outpost", TeleportToNerestOutpost, "Teleport to nerest outpost.");
    }

    public void RegisterCommand(string command, CommandHandler handler, string help)
    {
        commands.Add(command, new CommandRegistration(command, handler, help));
    }

    public void AppendLogLine(string line)
    {
        Debug.Log(line);

        if (scrollback.Count >= scrollbackSize) {
            scrollback.Dequeue();
        }
        scrollback.Enqueue(line);

        Log = scrollback.ToArray();
        LogChanged?.Invoke(Log);
    }

    public void RunCommandString(string commandString)
    {
        if (echo)
            AppendLogLine("$ " + commandString);

        string[] commandSplit = ParseArguments(commandString);
        string[] args = new string[0];
        if (commandSplit.Length < 1) {
            AppendLogLine(string.Format("Unable to process command '{0}'", commandString));
            return;

        } else if (commandSplit.Length >= 2) {
            int numArgs = commandSplit.Length - 1;
            args = new string[numArgs];
            Array.Copy(commandSplit, 1, args, 0, numArgs);
        }
        RunCommand(commandSplit[0].ToLower(), args);
        commandHistory.Add(commandString);
    }

    public void RunCommand(string command, string[] args)
    {
        if (!commands.TryGetValue(command, out CommandRegistration reg)) {
            AppendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", command));
        } else {
            if (reg.Handler == null) {
                AppendLogLine(string.Format("Unable to process command '{0}', handler was null.", command));
            } else {
                reg.Handler(args);
            }
        }
    }

    static string[] ParseArguments(string commandString)
    {
        LinkedList<char> parmChars = new LinkedList<char>(commandString.ToCharArray());
        bool inQuote = false;
        var node = parmChars.First;
        while (node != null) {
            var next = node.Next;
            if (node.Value == '"') {
                inQuote = !inQuote;
                parmChars.Remove(node);
            }
            if (!inQuote && node.Value == ' ') {
                node.Value = '\n';
            }
            node = next;
        }
        char[] parmCharsArr = new char[parmChars.Count];
        parmChars.CopyTo(parmCharsArr, 0);
        return (new string(parmCharsArr)).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    }

    #region Command handlers
    //Implement new commands in this region of the file.

    //A test command to demonstrate argument checking/parsing.
    //Will repeat the given word a specified number of times.
    /*
    void Babble(string[] args)
    {
        if (args.Length < 2) {
            AppendLogLine("Expected 2 arguments.");
            return;
        }
        string text = args[0];
        if (string.IsNullOrEmpty(text)) {
            AppendLogLine("Expected arg1 to be text.");
        } else {
            if (!Int32.TryParse(args[1], out int repeat)) {
                AppendLogLine("Expected an integer for arg2.");
            } else {
                for (int i = 0; i < repeat; ++i) {
                    AppendLogLine(string.Format("{0} {1}", text, i));
                }
            }
        }
    }
    */
    void Echo(string[] args)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string arg in args) {
            sb.AppendFormat("{0},", arg);
        }
        sb.Remove(sb.Length - 1, 1);
        AppendLogLine(sb.ToString());
    }

    void Help(string[] args)
    {
        //StringBuilder sb = new StringBuilder();
        foreach (CommandRegistration reg in commands.Values) {
            //sb.Append(string.Format("{0}: \t{1}\n", reg.Command, reg.Help));
            AppendLogLine(string.Format("{0}: \t{1}", reg.Command, reg.Help));
        }
        //AppendLogLine(sb.ToString());
    }

    void RepeatCommand(string[] args)
    {
        for (int cmdIdx = commandHistory.Count - 1; cmdIdx >= 0; --cmdIdx) {
            string cmd = commandHistory[cmdIdx];
            if (String.Equals(repeatCmdName, cmd)) {
                continue;
            }
            RunCommandString(cmd);
            break;
        }
    }

    void Reload(string[] args)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void EchoSetting(string[] args)
    {
        if (args[0] == "true" || args[0] == "on") {
            echo = true;
            AppendLogLine("echo set to true");
        } else if (args[0] == "false" || args[0] == "off") {
            echo = false;
            AppendLogLine("echo set to false");
        }
    }

    void PlayerHealth(string[] args)
    {
        if (!Int32.TryParse(args[0], out int hp)) {
            AppendLogLine("Expected an integer.");
            return;
        }

        Health h = PlayerMovement.playerObj.GetComponent<Health>();

        if (args.Length > 1) {
            if (!Int32.TryParse(args[1], out int maxHp)) {
                AppendLogLine("Expected an integer.");
                return;
            }
            h.MaxHealth = maxHp;
        }
        h.GetHealth = hp;
    }

    void FullScreen(string[] args)
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    void ExperianceAdd(string[] args)
    {
        if (!Int32.TryParse(args[0], out int points)) {
            AppendLogLine("Expected an integer.");
            return;
        }
        ExperiancePoints.Experiance += points;

    }
    void ExperianceAddPermanent(string[] args)
    {
        if (!Int32.TryParse(args[0], out int points)) {
            AppendLogLine("Expected an integer.");
            return;
        }
        ExperiancePoints.PermanentExperiance += points;

    }

    void Teleport(string[] args)
    {

        if (!float.TryParse(args[0], out float posx)) {
            AppendLogLine("Expected an integer.");
            return;
        }
        if (!float.TryParse(args[1], out float posy)) {
            AppendLogLine("Expected an integer.");
            return;
        }

        PlayerMovement.playerObj.transform.position = new Vector3(posx, posy);
    }

    void TeleportToExit(string[] args)
    {
        Cell[] cells = WorldGenerationHandler.instance.gameObject.GetComponentsInChildren<Cell>();
        foreach (Cell cell in cells) {
            if (cell.type == Enum.CellType.End) {
                PlayerMovement.playerObj.transform.position = cell.transform.position;
                return;
            }
        }
    }

    void TeleportToNerestOutpost(string[] args)
    {
        Cell[] cells = WorldGenerationHandler.instance.gameObject.GetComponentsInChildren<Cell>();
        List<Cell> outposts = new List<Cell>();
        foreach (Cell cell in cells) {
            if (cell.type == Enum.CellType.Outpost) {
                outposts.Add(cell);
            }
        }
        int id = 0;
        float dist = Vector3.Distance(PlayerMovement.playerObj.transform.position, outposts[0].transform.position);
        for (int i = 1; i < outposts.Count; i++) {
            float d = Vector3.Distance(PlayerMovement.playerObj.transform.position, outposts[i].transform.position);
            if (d < dist) {
                dist = d;
                id = i;
            }
        }
        PlayerMovement.playerObj.transform.position = outposts[id].transform.position;
    }

    void Clear(string[] args)
    {
        scrollback = new Queue<string>(scrollbackSize);
    }

    void Kill(string[] args)
    {
        if (args.Length > 0 && float.TryParse(args[0], out float t)) {
            GameObject.Destroy(PlayerMovement.playerObj, t);
            return;
        }
        PlayerMovement.playerObj.GetComponent<Health>().Die();
    }

    #endregion
}