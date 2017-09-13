using UAC.ERP.Common.Controls;

namespace UAC.ERP.Sales.PO.ViewModels
{
    public interface IViewModel
    {
        string Caption { get; }

        IViewModel Create();

        void OnMsgReceive(Messages msg);

        void GetAppBarButtons();
    }
}
