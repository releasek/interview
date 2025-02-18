using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace interview
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // ✅ 啟用 Attribute Routing
            config.MapHttpAttributeRoutes();

            // ✅ 設定 Web API 預設路由
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // ✅ 設定 JSON 回應格式，避免循環引用錯誤
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // ✅ 忽略循環引用
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // ✅ 使用 CamelCase 命名法
            config.Formatters.Remove(config.Formatters.XmlFormatter); // ✅ 移除 XML，讓 API 只回傳 JSON
        }
    }
}
