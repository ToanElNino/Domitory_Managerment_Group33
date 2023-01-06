using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enum
{
    public static partial class CommonENum
    {
        public enum STATE_RRERGIS_ROOM_FORM
        {
            STUDENT_CANCELED = 2,
            ADMIN_CANCELED = 3,
            NEW = 0,
            CONFIRMED = 1,
        }
        public enum TYPE_ORDER
        {
            [EnumDisplayString("Order hàng hóa tiêu chuẩn")]
            PHYSICAL_ORDER = 1
            ,
            [EnumDisplayString("Order đặt sân bóng, đặt khách sạn,...")]
            RESERVATION_ORDER = 2
            ,
            [EnumDisplayString("Order chỉ có các thông tin cho 2 bên như dịch vụ sửa xe,...")]
            INFORMATIONAL_ORDER = 3

        }
        public enum FORM_ID_ORDER
        {
            [EnumDisplayString("Form admin getall order")]
            FORM_ADMIN_GET_ORDER_GETALL = 1
            ,
            [EnumDisplayString("Form admin getall orders new")]
            FORM_ADMIN_GET_ORDER_NEW = 11
            ,
            [EnumDisplayString("Form admin getall orders canceled")]
            FORM_ADMIN_GET_ORDER_ACTIVE = 12
            ,
            [EnumDisplayString("Form admin getall orders confirmed")]
            FORM_ADMIN_ORDER_REFUSE = 13
            ,
            [EnumDisplayString("Form partner get all order")]
            FORM_PARTNER_ORDER_GETALL = 2
            ,
            [EnumDisplayString("Form partner getall orders new")]
            FORM_PARTNER_ORDER_NEW = 21
            ,
            [EnumDisplayString("Form partner get a orders confirmed ")]
            FORM_PARTNER_ORDER_CONFIRMED = 22
            ,
            [EnumDisplayString("Form partner get a orders canceled ")]
            FORM_PARTNER_ORDER_SHIPPING = 23
            ,
            [EnumDisplayString("Form partner get a orders canceled ")]
            FORM_PARTNER_ORDER_CANCELED = 24
            ,
        }

    }

    public class EnumDisplayString : Attribute
    {
        public string DisplayString;

        public EnumDisplayString(string text)
        {
            this.DisplayString = text;
        }
    }
}
