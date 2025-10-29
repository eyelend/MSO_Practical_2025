using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P3_Forms
{
    internal class ControlSubset
    {
        protected readonly Control.ControlCollection fullControlCollection;
        ICollection<Control> items;

        public ControlSubset(Control.ControlCollection fullControlCollection, ICollection<Control> gridItems)
        {
            this.fullControlCollection = fullControlCollection;
            if (gridItems.Count != 0)
                throw new Exception("gridItems should start empty.");
            this.items = gridItems;
        }

        public void AddItem(Control item)
        {
            if (item == null) return;
            fullControlCollection.Add(item);
            item.BringToFront();
            items.Add(item);
        }

        public void RemoveItem(Control item)
        {
            fullControlCollection.Remove(item);
            items.Remove(item);
        }

        public void Clear()
        {
            while (true)
            {
                Control? itemToRemove = items.FirstOrDefault();
                if (itemToRemove == null) break; // out of items
                RemoveItem(itemToRemove);

            }
            /*foreach (Control item in items)
                fullControlCollection.Remove(item);
            items.Clear();*/
        }
    }
}
