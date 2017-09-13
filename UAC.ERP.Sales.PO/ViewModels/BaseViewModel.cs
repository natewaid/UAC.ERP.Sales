using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UAC.ERP.Common.Controls;
using UAC.ERP.Common.Controls.ViewModels;
using UAC.ERP.Sales.Business.Operation;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.PO.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        public IDictionary<MessageType, Action> MessageCommands { get; set; } = new Dictionary<MessageType, Action>();

        public IPOHelper PoHelper { get; set; } = new POHelper();

        public IStatusHelper StatusHelper { get; set; } = new StatusHelper();

        public ITermHelper TermHelper { get; set; } = new TermHelper();        

        public virtual ObservableCollection<AppBarControlModel> AppBarButtons { get; set; }

        public abstract string Caption { get; }   

        public abstract IViewModel Create();        

        public abstract void OnMsgReceive(Messages msg);

        public abstract void GetAppBarButtons();

        public virtual ObservableCollection<KeyValuePair<int, string>> StatusItems
        {
            get
            {
                var statuses = StatusHelper.GetAllStatuses();
                if (!statuses.Any())
                {
                    return new ObservableCollection<KeyValuePair<int, string>>();
                }

                return statuses.ToObservableCollectionOfKvp<Status, int, string>
                    (
                        nameof(Status.Id),
                        nameof(Status.Description)
                    );
            }
        }

        public virtual ObservableCollection<KeyValuePair<int, string>> PaymentTermItems
        {
            get
            {
                return GetTermItems("Payment");
            }
        }

        public virtual ObservableCollection<KeyValuePair<int, string>> DeliveryTermItems
        {
            get
            {
                return GetTermItems("Delivery");
            }
        }        

        private ObservableCollection<KeyValuePair<int, string>> GetTermItems(string termType)
        {
            IEnumerable<Term> terms = TermHelper.GetAllTerms();
            var types = TermHelper.GetAllTermTypes();

            if (!terms.Any())
            {
                return new ObservableCollection<KeyValuePair<int, string>>();
            }

            if (string.IsNullOrEmpty(termType))
            {
                return ConvertTermItems(terms);
            }
                        
            var type = types.FirstOrDefault(t => t.Description.Equals(termType));

            if (type == null)
            {
                return ConvertTermItems(terms);
            }

            terms = terms.Where(t => t.TermType.Equals(type.Id));

            return ConvertTermItems(terms);
        }

        private ObservableCollection<KeyValuePair<int, string>> ConvertTermItems(IEnumerable<Term> terms)
        {
            if (!terms.Any())
            {
                return new ObservableCollection<KeyValuePair<int, string>>();
            }

            return terms.ToObservableCollectionOfKvp<Term, int, string>
                (
                    nameof(Term.Id),
                    nameof(Term.Code)
                );
        }
    }
}
