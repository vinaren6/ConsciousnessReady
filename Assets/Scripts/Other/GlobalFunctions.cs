using System.Collections;
using System.Collections.Generic;

public static class GlobalFunctions
{
    static public T[] AddElementToArray<T>(T[] array, T element)
    {
        T[] newArray = new T[array.Length + 1];
        int i;
        for (i = 0; i < array.Length; i++) {
            newArray[i] = array[i];
        }
        newArray[i] = element;
        return newArray;
    }
}
