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
        ICollection<Control> gridItems;

        public ControlSubset(Control.ControlCollection fullControlCollection, ICollection<Control> gridItems)
        {
            this.fullControlCollection = fullControlCollection;
            if (gridItems.Count != 0)
                throw new Exception("gridItems should start empty.");
            this.gridItems = gridItems;
        }

        public void AddItem(Control item)
        {
            if (item == null) return;
            fullControlCollection.Add(item);
            item.BringToFront();
            gridItems.Add(item);
        }

        public void RemoveItem(Control item)
        {
            fullControlCollection.Remove(item);
            gridItems.Remove(item);
        }

        public void Clear()
        {
            while (true)
            {
                Control? itemToRemove = gridItems.FirstOrDefault();
                if (itemToRemove == null) break; // out of items
                RemoveItem(itemToRemove);

            }
            /*foreach (Control item in gridItems)
                fullControlCollection.Remove(item);
            gridItems.Clear();*/
        }
    }
}
