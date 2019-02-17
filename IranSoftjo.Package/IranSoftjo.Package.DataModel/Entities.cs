using Mehr.Reflection;

namespace IranSoftjo.Package.DataModel
{
    public partial class PageGroup
    {
        public PageGroupTypeEnum TypeLogEnum
        {
            get { return (PageGroupTypeEnum)PageGroupType; }
            set { PageGroupType = (byte)value; }
        }
    }

    public enum PageGroupTypeEnum
    {
        [EnumItem("متنی تک صفحه ای")]
        Text = 0,
        [EnumItem("تبی")]
        Tab = 1,
        [EnumItem("صفحه بندی")]
        Pager = 2,
        [EnumItem("کرکره ای")]
        PanelBar = 3,
        [EnumItem("کتابچه")]
        Book = 4,
    }

    public partial class LoginLog
    {
        public TypeLogEnum TypeLogEnum
        {
            get { return (TypeLogEnum)TypeLog; }
            set { TypeLog = (byte)value; }
        }
    }

    public partial class RequestEvaluation
    {
        public ProjectTypeEnum ProjectTypeEnum
        {
            get
            {
                if (ProjectType != null)
                    return (ProjectTypeEnum)ProjectType;
                return ProjectTypeEnum.Other;
            }
            set { ProjectType = (byte)value; }
        }

        public AmountEnum AmountEnum
        {
            get
            {
                if (Amount != null)
                    return (AmountEnum)Amount;
                return AmountEnum.Amount0;
            }
            set { Amount = (byte)value; }
        }
    }

    public enum AmountEnum
    {
        [EnumItem("100 - 500  هزارتومان")]
        Amount0 = 0,

        [EnumItem("500 - 999 هزارتومان")]
        Amount1 = 1,

        [EnumItem("1 - 2 میلیون تومان")]
        Amount2 = 2,

        [EnumItem("2 - 4 میلیون تومان")]
        Amount3 = 3,

        [EnumItem("4 - 10 میلیون تومان")]
        Amount4 = 4,

        [EnumItem("10 میلیون تومان به بالا ")]
        Amount5 = 5,
    }

    public enum ProjectTypeEnum
    {
        [EnumItem("سیستم مدیریت محتوا - CMS")]
        Cms = 0,
        [EnumItem("فروشگاه اینترنتی")]
        Shoping = 1,
        [EnumItem("پورتال - Portal")]
        Portal = 2,
        [EnumItem("طراحی وب سایت")]
        WebDesign = 3,
        [EnumItem("طراحی مجدد وب سایت قبلی ام")]
        WebsiteRedesign = 4,
        [EnumItem("نرم افزار سفارشی تحت وب")]
        CustomWebSoftware = 5,
        [EnumItem("نرم افزار سفارشی تحت ویندوز")]
        CustomWindowsSoftware = 6,

        [EnumItem("غیره")]
        Other = 5,
    }

    public enum TypeLogEnum
    {
        [EnumItem("ورود")]
        Login = 0,
        [EnumItem("خروج")]
        Logout = 1,
    }

    public enum OperatingSystemEnum
    {
        [EnumItem("ویندوز")]
        Win = 0,
        [EnumItem("وب")]
        Web = 1,
        [EnumItem("اندروید")]
        Android = 2,
        [EnumItem("IOS")]
        IOS = 3,
        [EnumItem("ویندوز موبایل")]
        WinMobile = 4,
    }

    public enum SoftwarePriceTypeEnum
    {
        [EnumItem("شروع قیمت")]
        Start = 0,
        [EnumItem("قیمت کل")]
        Total = 1,
        [EnumItem("تماس بگیرید")]
        Call = 2,
    }

    public enum SoftwareUserLicenseEnum
    {
        [EnumItem("خرید")]
        Buye = 0,
        [EnumItem("رایگان")]
        Free = 1,
        [EnumItem("اجاره")]
        Call = 2,
    }

    public enum SoftwareStatusEnum
    {
        [EnumItem(" در انتظار تایید")]
        First = 0,
        [EnumItem("تایید شده و فعال")]
        Active = 1,
        [EnumItem("غیرفعال")]
        DisActive = 2,
        [EnumItem("درخواست حذف")]
        RequestDelete = 3,
    }

    public partial class Software
    {
        public SoftwareStatusEnum SoftwareStatusEnum
        {
            get { return (SoftwareStatusEnum)SoftwareStatus; }
            set { SoftwareStatus = (byte)value; }
        }
    }


    public partial class Order
    {
        public IsFinalizedEnum IsFinalizedEnum
        {
            get { return (IsFinalizedEnum)IsFinalized; }
            set { IsFinalized = (byte)value; }
        }
    }

    public partial class PaymentLog
    {
        public BankTypeEnum BankTypeEnum
        {
            get
            {
                if (BankType != null) return (BankTypeEnum)BankType;
                return BankTypeEnum.None;
            }
            set { BankType = (byte)value; }
        }
        public PaymentTypeEnum PaymentTypeEnum
        {
            get
            {
                if (PaymentType != null) return (PaymentTypeEnum)PaymentType;
                return PaymentTypeEnum.None;
            }
            set { PaymentType = (byte)value; }
        }
    }

    public enum PaymentTypeEnum : byte
    {
        [EnumItem("نامشخص")]
        None = 0,
        [EnumItem("پرداخت الکترونیک")]
        PaymentOnline = 1,
        [EnumItem("از موجودی")]
        PaymentBalance = 2,
        [EnumItem("با فیش بانکی")]
        BankDraft = 3,
        [EnumItem("کارت به کارت")]
        Pos = 4,
    }

    public enum BankTypeEnum : byte
    {
        [EnumItem("نامشخص")]
        None = 0,
        [EnumItem("زرین پال")]
        Zarinpal = 1,
        [EnumItem("بانک پاسارگاد")]
        Passargad = 2,
        [EnumItem("بانک سامان")]
        Saman = 3,
        [EnumItem("بانک ملت")]
        Mellat = 4,
    }

    public enum IsFinalizedEnum : byte
    {
        [EnumItem("پرداخت نشده")]
        Unpaid = 0,
        [EnumItem("در انتظار تایید")]
        Waiting = 1,
        [EnumItem("پرداخت تایید شده")]
        Paid = 2,
    }
}