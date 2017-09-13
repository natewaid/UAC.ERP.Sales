using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using UAC.ERP.Sales.Business.Models;
using System.Collections.ObjectModel;
using UAC.ERP.Common.Controls.ViewModels;
using UAC.ERP.Common.Controls;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UAC.ERP.Customer.ViewModels
{
    [POCOViewModel]
    public class AddEditCustomerViewModel
    {
        #region Virtual Properties

        public virtual CustomerModels CustomerSelectedItem { get; set; }
        public virtual ObservableCollection<AppBarControlModel> AppBarButtons { get; set; }
        public virtual ObservableCollection<SimpleCountryModel> CountryList { get; set; }
        public virtual ObservableCollection<US_State> StateList { get; set; }
        public virtual SimpleCountryModel CountrySelectedItem { get; set; }
        #endregion

        #region Constructor

        public AddEditCustomerViewModel()
        {
            Messenger.Default.Register<Messages>(this, OnMsgReceive);
        }

        public static AddEditCustomerViewModel Create()
        {
            return ViewModelSource.Create(() => new AddEditCustomerViewModel());
        }
        #endregion

        #region Message
        private void OnMsgReceive(Messages msg)
        {
            switch (msg.MessageTypes)
            {
                case MessageType.AddCustomer:

                    CustomerSelectedItem = new CustomerModels();
                    CountryList = new ObservableCollection<SimpleCountryModel>(GetCountries());
                    StateList = new ObservableCollection<US_State>(StateArray.States());
                    break;

                case MessageType.EditCustomer:

                    CustomerSelectedItem = (CustomerModels)msg.AllObject;

                    break;

                default:
                    break;

            }
        }
        #endregion

        #region AppBarButtons

        private void GetEditBarButtons()
        {
            AppBarButtons = new ObservableCollection<AppBarControlModel>();
            AppBarButtons.Add(
                        new AppBarControlModel
                        {
                            Label = "Save",
                            Enabled = true,
                            Alignment = "Center",
                            IsSeparator = false,
                            ExecutingCommand = new DelegateCommand(SaveCustomer),

                        });
            AppBarButtons.Add(
                new AppBarControlModel
                {
                    Label = "Cancel",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(CancelCustomer),

                });

        }

        #endregion

        #region Function

        private void CancelCustomer()
        {
            throw new NotImplementedException();
        }

        private void SaveCustomer()
        {
            throw new NotImplementedException();
        }

        public void LoadCountrys()
        {
            CountryList = new ObservableCollection<SimpleCountryModel>(GetCountries());
        }
        public ObservableCollection<SimpleCountryModel> GetCountries()
        {
            int count = 0;
            var newQuery = (from ri in
                                from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                                select new RegionInfo(ci.LCID)
                            group ri by ri.TwoLetterISORegionName into g
                            orderby g.First().DisplayName ascending
                            select new SimpleCountryModel
                            {
                                Id = ++count,
                                Country = g.First().DisplayName
                            });


            ObservableCollection<SimpleCountryModel> SimpCountryList = new ObservableCollection<SimpleCountryModel>(newQuery);

            return SimpCountryList;
        }
       
        #endregion
    }

    #region StateList

    public class StateArray
    {
     
       static public List<US_State> States()
        {
            List<US_State> states;

            states = new List<US_State>(50);
            states.Add(new US_State(1, "Alabama"));
            states.Add(new US_State(2, "Alaska"));
            states.Add(new US_State(3, "Arizona"));
            states.Add(new US_State(4, "Arkansas"));
            states.Add(new US_State(5, "California"));
            states.Add(new US_State(6, "Colorado"));
            states.Add(new US_State(7, "Connecticut"));
            states.Add(new US_State(8, "Delaware"));
            states.Add(new US_State(9, "District Of Columbia"));
            states.Add(new US_State(10, "Florida"));
            states.Add(new US_State(11, "Georgia"));
            states.Add(new US_State(12, "Hawaii"));
            states.Add(new US_State(13, "Idaho"));
            states.Add(new US_State(14, "Illinois"));
            states.Add(new US_State(15, "Indiana"));
            states.Add(new US_State(16, "Iowa"));
            states.Add(new US_State(17, "Kansas"));
            states.Add(new US_State(18, "Kentucky"));
            states.Add(new US_State(19, "Louisiana"));
            states.Add(new US_State(20, "Maine"));
            states.Add(new US_State(21, "Maryland"));
            states.Add(new US_State(22, "Massachusetts"));
            states.Add(new US_State(23, "Michigan"));
            states.Add(new US_State(24, "Minnesota"));
            states.Add(new US_State(25, "Mississippi"));
            states.Add(new US_State(26, "Missouri"));
            states.Add(new US_State(27, "Montana"));
            states.Add(new US_State(28, "Nebraska"));
            states.Add(new US_State(29, "Nevada"));
            states.Add(new US_State(30, "New Hampshire"));
            states.Add(new US_State(31, "New Jersey"));
            states.Add(new US_State(32, "New Mexico"));
            states.Add(new US_State(33, "New York"));
            states.Add(new US_State(34, "North Carolina"));
            states.Add(new US_State(35, "North Dakota"));
            states.Add(new US_State(36, "Ohio"));
            states.Add(new US_State(37, "Oklahoma"));
            states.Add(new US_State(38, "Oregon"));
            states.Add(new US_State(39, "Pennsylvania"));
            states.Add(new US_State(40, "Rhode Island"));
            states.Add(new US_State(41, "South Carolina"));
            states.Add(new US_State(42, "South Dakota"));
            states.Add(new US_State(43, "Tennessee"));
            states.Add(new US_State(44, "Texas"));
            states.Add(new US_State(45, "Utah"));
            states.Add(new US_State(46, "Vermont"));
            states.Add(new US_State(47, "Virginia"));
            states.Add(new US_State(48, "Washington"));
            states.Add(new US_State(49, "West Virginia"));
            states.Add(new US_State(50, "Wisconsin"));
            states.Add(new US_State(51, "Wyoming"));
            return states;
        }

    }

   public class US_State
    {

        public US_State()
        {
            Name = null;
            Id = 0;
        }

        public US_State(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}
    #endregion
