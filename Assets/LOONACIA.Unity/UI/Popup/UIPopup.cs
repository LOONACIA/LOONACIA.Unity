using LOONACIA.Unity.Managers;

namespace LOONACIA.Unity.UI
{
    public class UIPopup : UIBase
    {
        protected override void Init()
        {
            Manager.UI.SetCanvas(gameObject, true);
        }

        public virtual void Close()
        {
            Manager.UI.ClosePopupUI(this);
        }
    }
}