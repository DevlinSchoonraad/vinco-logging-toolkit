﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite_Sample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMakeException_Click(object sender, EventArgs e)
        {
            // Make an exception

            int i = 0;
            int result = 10 / i;
        }
    }
}