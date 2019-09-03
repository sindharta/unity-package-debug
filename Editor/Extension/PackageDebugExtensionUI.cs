using System.IO;
using UnityEngine;
using UnityEditor.PackageManager;

#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;
#endif

namespace org.shin.package_debug.Editor {
#if UNITY_2019_1_OR_NEWER
    internal class PackageDebugExtensionUI: VisualElement {

    internal PackageDebugExtensionUI(VisualTreeAsset asset) {
        m_root = asset.CloneTree();
        string path = UnityEditor.EditorGUIUtility.isProSkin ? Constants.DARK_STYLE_PATH : Constants.LIGHT_STYLE_PATH;
        var styleSheet = UnityEditor.EditorGUIUtility.Load(path) as StyleSheet;
        m_root.styleSheets.Add(styleSheet);
        Add(m_root);

        CheckUnusedImagesInDocButton.clickable.clicked += CheckUnusedImagesInDoc;
    }

//---------------------------------------------------------------------------------------------------------------------
    public void OnPackageSelectionChange(PackageInfo packageInfo) {
        if (m_root == null)
            return;
        
        m_packageInfo = packageInfo;

    }

//---------------------------------------------------------------------------------------------------------------------
    private void CheckUnusedImagesInDoc() {
        if (m_root == null)
            return;

        string[] searchPatterns = {
            "*.jpg", 
            "*.png",
            "*.jpeg", 
        };


        string DOC_PATH = Constants.PACKAGE_ROOT_PATH + m_packageInfo.name + "/Documentation~/";


        string[] mdFiles = Directory.GetFiles(DOC_PATH, "*.md", SearchOption.AllDirectories);
        int numMDFiles = mdFiles.Length;

        int numSearchPatterns = searchPatterns.Length;
        for (int i=0;i<numSearchPatterns;++i) {
            string[] imageFiles = Directory.GetFiles(DOC_PATH, searchPatterns[i], SearchOption.AllDirectories);
            int numImageFiles = imageFiles.Length;

            for (int j=0;j<numImageFiles;++j) {
                string curImageFile = Path.GetFileName(imageFiles[j]); //only consider the filename for now;
                
                bool imageUsed = false;
                for (int k=0;k<numMDFiles && !imageUsed;++k) {
                    string curMDFile = mdFiles[k];
                    if(File.ReadAllText(curMDFile).Contains(curImageFile)) {
                        imageUsed = true;
                    }
                }

                if (!imageUsed) 
                    Debug.LogWarning("Image is not used: " + curImageFile);

            }
        }
    }

//---------------------------------------------------------------------------------------------------------------------


    internal Button CheckUnusedImagesInDocButton { get { return m_root.Q<Button>("checkUnusedImagesInDoc"); } }

//---------------------------------------------------------------------------------------------------------------------

    private VisualElement m_root;
    private PackageInfo m_packageInfo;

}
#endif

} //namespace org.shin.package_debug.Editor
