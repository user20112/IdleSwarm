using System;
using System.Collections.Generic;
using System.Text;

namespace IdleSwarm.Classes
{
    public interface DrawerPage
    {
        string PageTitle { get; set; }
        int PageID { get; set; }
    }
}
