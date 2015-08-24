using System.Windows.Forms;

namespace Sleek_Bill.Common
{
    public class Common
    {
        public static void SetFormCoordinate(Form form)
        {
            form.Left = (form.MdiParent.ClientRectangle.Width - form.Width) / 2;
            form.Top = (form.MdiParent.ClientRectangle.Height - form.Height) / 2;
        }

        public static void SetDialogCoordinate(Form form)
        {
            var parentForm = form.Owner.MdiChildren[0];
            form.Left = parentForm.Left;
            form.Top = parentForm.Top;
        }
    }
}
