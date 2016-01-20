namespace Automation.Selenium.Tests.Common.Utils
{
    public class Constants
    {


        public enum SearchResultColoumnName
        {
            Id,
            Name,
            Ref,
            Definition,
            Tags
        }
        public enum HttpStatusCode
        {
            Accepted = 0xca,
            Ambiguous = 300,
            BadGateway = 0x1f6,
            BadRequest = 400,
            Conflict = 0x199,
            Continue = 100,
            Created = 0xc9,
            ExpectationFailed = 0x1a1,
            Forbidden = 0x193,
            Found = 0x12e,
            GatewayTimeout = 0x1f8,
            Gone = 410,
            HttpVersionNotSupported = 0x1f9,
            InternalServerError = 500,
            LengthRequired = 0x19b,
            MethodNotAllowed = 0x195,
            Moved = 0x12d,
            MovedPermanently = 0x12d,
            MultipleChoices = 300,
            NoContent = 0xcc,
            NonAuthoritativeInformation = 0xcb,
            NotAcceptable = 0x196,
            NotFound = 0x194,
            NotImplemented = 0x1f5,
            NotModified = 0x130,
            OK = 200,
            PartialContent = 0xce,
            PaymentRequired = 0x192,
            PreconditionFailed = 0x19c,
            ProxyAuthenticationRequired = 0x197,
            Redirect = 0x12e,
            RedirectKeepVerb = 0x133,
            RedirectMethod = 0x12f,
            RequestedRangeNotSatisfiable = 0x1a0,
            RequestEntityTooLarge = 0x19d,
            RequestTimeout = 0x198,
            RequestUriTooLong = 0x19e,
            ResetContent = 0xcd,
            SeeOther = 0x12f,
            ServiceUnavailable = 0x1f7,
            SwitchingProtocols = 0x65,
            TemporaryRedirect = 0x133,
            Unauthorized = 0x191,
            UnsupportedMediaType = 0x19f,
            Unused = 0x132,
            UseProxy = 0x131
        }

        public enum Page
        {
            Home,
            Bag
        }

        //public struct LoginValidation
        //{
        //    public const string NoPassword = "The Password can't be null or empty.";
        //    public const string NoEmail = "Email can't be null or empty.";
        //}

        public struct RgbaColour
        {
            public const string Red = "rgba(255, 238, 238, 1)";
        }

        public enum Colour
        {
            Red
        }

        /// <summary>
        ///     Page Title
        /// </summary>
        public struct PageTitle
        {
            public const string Main = "Insight v";
            public const string Login = "Login";
        }

        public struct HttpCustomResponse
        {
            public string Body;
            public int Code;
        }
    }
}