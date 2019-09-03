using UnityEditor.PackageManager;

#if UNITY_2019_1_OR_NEWER
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;
#endif

namespace org.shin.package_debug.Editor {
#if !UNITY_PACKAGE_MANAGER_DEVELOP_EXISTS
#if UNITY_2019_1_OR_NEWER

    [UnityEditor.InitializeOnLoad]
internal class PackageDebugExtension : IPackageManagerExtension {

    public PackageDebugExtension()     {
    }
//---------------------------------------------------------------------------------------------------------------------

    public VisualElement CreateExtensionUI()     {

        if (null!=m_ui)
            return m_ui;

        VisualTreeAsset template = UnityEditor.AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(Constants.TEMPLATE_PATH);
        m_ui = new PackageDebugExtensionUI(template);

        return m_ui ?? new VisualElement();
    }

//---------------------------------------------------------------------------------------------------------------------


    public void OnPackageSelectionChange(PackageInfo packageInfo) {
        if (packageInfo == m_packageInfo)
            return;

        if (m_ui == null)
            return;

        m_packageInfo = packageInfo;
        m_ui.OnPackageSelectionChange(m_packageInfo);
    }

//---------------------------------------------------------------------------------------------------------------------
    public void OnPackageAddedOrUpdated(PackageInfo packageInfo)
    {
    }

//---------------------------------------------------------------------------------------------------------------------
    public void OnPackageRemoved(PackageInfo packageInfo)
    {
    }

//---------------------------------------------------------------------------------------------------------------------

    static PackageDebugExtension()
    {
        PackageManagerExtensions.RegisterExtension(new PackageDebugExtension());
    }

//---------------------------------------------------------------------------------------------------------------------

    private PackageInfo m_packageInfo;
    private PackageDebugExtensionUI m_ui;

}

#endif
#endif

} //namespace org.shin.package_debug.Editor
