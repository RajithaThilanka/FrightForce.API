using Json.Schema;
using System.Text.Json;

namespace FrightForce.Infractructure.Utils.Json
{
    public interface IJsonUtils
    {
        EvaluationResults ValidateSchema(string json);

        EvaluationResults ValidateSchema(JsonDocument schemaDef);
    }
}