using LOONACIA.Unity.Managers;

namespace LOONACIA.Unity.UI
{
    public class UIPopup : UIBase
    {
        protected override void Init()
        {
            ManagerHost.UI.SetCanvas(gameObject, true);
        }

        public virtual void Close()
        {
            ManagerHost.UI.ClosePopupUI(this);
        }
    }
}