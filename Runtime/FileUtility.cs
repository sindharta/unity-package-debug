using System.IO;
using UnityEngine;

namespace org.shin.package_debug {

public static class FileUtility
{

    public static void DeleteAll(DirectoryInfo di) {
        try {
            foreach (FileInfo file in di.EnumerateFiles()) {
                file.Delete(); 
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories()) {
                dir.Delete(true); 
            }
        } catch {
            Debug.LogError("Error when trying to delete: " + di.FullName);
        }
    }

//---------------------------------------------------------------------------------------------------------------------

    //Copy Recursive
    public static void CopyAll(string sourceDir, string targetDir)
    {
        Directory.CreateDirectory(targetDir);

        DirectoryInfo sourceDI = new DirectoryInfo(sourceDir);
        foreach (FileInfo file in sourceDI.EnumerateFiles()) {
            File.Copy(file.FullName, Path.Combine(targetDir, file.Name));
        }
        foreach (DirectoryInfo dir in sourceDI.EnumerateDirectories()) {
            CopyAll(dir.FullName, Path.Combine(targetDir, dir.Name));
        }

    }
}

} //end namespace
