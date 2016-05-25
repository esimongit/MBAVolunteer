using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;


namespace MBAV 
{
    public static class Recursor
    {
         public static Control ContainsControlType(Control control, params Type[] types)
         {    
              foreach (Type type in types)    
              {      
                  if (control.GetType().Equals(type))        
                      return control;      
                  else             
                      foreach (Control ctrl in control.Controls)        
                      {         
                          Control tmpCtrl = ContainsControlType(ctrl, type);          
                          if (tmpCtrl != null)            
                              return tmpCtrl;        
                      }   
              }        
              return null;  
         }
         public static bool ContainsLink(Control control)  
         {    
             bool ret = false;        
             Control ctrl = ContainsControlType(control, typeof(HyperLink), typeof(LinkButton), typeof(DataBoundLiteralControl));   
             if (ctrl != null)    
             {      
                 if (ctrl.GetType().Equals(typeof(DataBoundLiteralControl)))      
                 {        
                     DataBoundLiteralControl dblc = (DataBoundLiteralControl)ctrl;        
                     if (dblc.Text.Contains("href") || dblc.Text.Contains("onclick"))   
                         ret = true;      
                 }      
                 else ret = true;    
             }    
             return ret; 
         }
    }
}
