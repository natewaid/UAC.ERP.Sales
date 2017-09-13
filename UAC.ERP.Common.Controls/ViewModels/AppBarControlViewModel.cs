using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace UAC.ERP.Common.Controls.ViewModels
{
    [POCOViewModel]
    public class AppBarControlModel : ViewModelBase
    {
        private string _label;

        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                _label = value;
                RaisePropertyChanged("Label");
            }
        }

        private string _labelHint;

        public string LabelHint
        {
            get
            {
                return _labelHint;
            }

            set
            {
                _labelHint = value;
                RaisePropertyChanged("LabelHint");
            }
        }

        private byte[] _picture;

        public byte[] Picture
        {
            get
            {
                return _picture;
            }

            set
            {
                _picture = value;
                RaisePropertyChanged("Picture");
            }
        }

        private bool _enabled;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = value;
                RaisePropertyChanged("Enabled");
            }
        }

        private bool _isSeparator;

        public bool IsSeparator
        {
            get
            {
                return _isSeparator;
            }

            set
            {
                _isSeparator = value;
                RaisePropertyChanged("IsSeparator");
            }
        }

        private DelegateCommand _executingCommand;
        
        public DelegateCommand ExecutingCommand
        {
            get
            {
                return _executingCommand;
            }

            set
            {
                _executingCommand = value;
                RaisePropertyChanged("ExecutingCommand");
            }
        }

        private string _alignment;

        public string Alignment
        {
            get
            {
                return _alignment;
            }

            set
            {
                _alignment = value;
                RaisePropertyChanged("Alignment");
            }
        }

        private string _content;

        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
                RaisePropertyChanged("Content");
            }
        }

        private bool? _visible;

        public bool? Visible
        {
            get
            {
                return _visible == null ? Enabled : _visible;
            }

            set
            {
                _visible = value;
                RaisePropertyChanged("Visible");
            }
        }

        public void propertyChangeExecute(string propName)
        {
            RaisePropertyChanged(propName);
        }
    }
}