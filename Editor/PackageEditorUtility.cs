using System.Reflection;

namespace org.shin.package_debug.Editor {

public class PackageEditorUtility 
{
    public static string[] GetPackagesPaths() {
        if (m_getPackagesPathsMethod == null) {
            Assembly assembly = typeof(UnityEditor.PackageManager.Client).Assembly;
            System.Type[] types = assembly.GetTypes();

            System.Type folderType = CollectionUtility.GetFirst(types, 
                (t) => { return (t.FullName == "UnityEditor.PackageManager.Folders"); }
            );

            if (null!=folderType) {
                m_getPackagesPathsMethod = folderType.GetMethod("GetPackagesPaths", BindingFlags.Public | BindingFlags.Static);
            }

            //error check
            if (null == m_getPackagesPathsMethod) {
                return null;
            }
        }
        return (string[]) m_getPackagesPathsMethod.Invoke(null, null);
    }

    internal static MethodInfo m_getPackagesPathsMethod = null;

}

} //end namespace
