using System;
using System.Collections.Generic;

// extends basic List<T> class to have Add event

namespace VPSeminarska.Libraries
{
    public class ObservableList<T> : List<T>
    {
        // create OnAdd event
        public event EventHandler<ObservableListOnAddEventArgs> OnAdd;

        public new void Add(T item)
        {
            // before adding the element to the list call the OnAdd event
            if (OnAdd != null)
            {
                OnAdd(this, new ObservableListOnAddEventArgs(item));
            }
            base.Add(item);
        }
    }

    // class for OnAdd event arguments
    // contains the item added to the list
    public class ObservableListOnAddEventArgs : EventArgs
    {
        public object Item;

        public ObservableListOnAddEventArgs(object item)
        {
            Item = item;
        }
    }
}
