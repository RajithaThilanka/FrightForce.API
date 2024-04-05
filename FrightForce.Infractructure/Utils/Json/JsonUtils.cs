using Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrightForce.Infractructure.Utils.Json
{
    public class JsonUtils : IJsonUtils
    {
        public EvaluationResults ValidateSchema(string schemaDef)
        {
            var schema = JsonSchema.FromText(schemaDef);
            return schema.Evaluate(schemaDef);
        }

        public EvaluationResults ValidateSchema(JsonDocument schemaDef)
        {
            var schemaString = schemaDef.RootElement.GetRawText();
            return ValidateSchema(schemaString);
        }
    }
}
