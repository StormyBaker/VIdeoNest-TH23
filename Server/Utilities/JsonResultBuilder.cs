using Microsoft.AspNetCore.Mvc;

namespace VideoNestServer.Utilities
{
    public class JsonResultBuilder
    {
        private Dictionary<string, object> result = new Dictionary<string, object>();
        
        public JsonResultBuilder() { 
            
        }

        public JsonResultBuilder set(string key, object value)
        {
            result[key] = value; 
            return this;
        }

        public JsonResult get() { return new JsonResult(result); }
    }
}
