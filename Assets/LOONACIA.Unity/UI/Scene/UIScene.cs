using LOONACIA.Unity.Managers;

namespace LOONACIA.Unity.UI
{
    public class UIScene : UIBase
    {
        protected override void Init()
        {
            Manager.UI.SetCanvas(gameObject, false);
        }
    }
}