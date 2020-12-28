using System.IO;

public static class ExperiancePoints
{
    private static readonly string fileName = "data.bin"; 


    private static int experiance = 0;
    private static int permanentExperiance = 0;
    private static int usedExperiance = 0;
    private static int usedPermanentExperiance = 0;

    public static int Experiance { get => experiance; set => experiance = value < 0 ? 0 : value; }
    public static int PermanentExperiance { get => permanentExperiance; set => permanentExperiance = value < 0 ? 0 : value; }
    public static int UsedExperiance { get => usedExperiance; }
    public static int UsedPermanentExperiance { get => usedPermanentExperiance; }
    public static bool UseExperiance(int amount) { if (experiance < amount) return false; experiance -= amount; usedExperiance += amount; return true; }
    public static bool UsePermanentExperiance(int amount) { if (permanentExperiance < amount) return false; permanentExperiance -= amount; usedPermanentExperiance += amount; return true; }

    public static void Reset()
    {
        experiance = 0;
        usedExperiance = 0;
    }

    public static void Save()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create))) {
            writer.Write(permanentExperiance);
            writer.Write(usedPermanentExperiance);
        }
    }
    public static void Load()
    {
        if (File.Exists(fileName)) {
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open))) {
                permanentExperiance = reader.ReadInt32();
                usedPermanentExperiance = reader.ReadInt32();
            }
        }
    }
}
