using GameServer.Infrastructure.MessageBroker.Enums;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace GameServer.Infrastructure.MessageBroker.Models
{
    public class GameMessage
    {
        public string GameId { get; set; }
        public Guid GameObjectId { get; set; }
        public OperationType OperationId { get; set; }
        public JsonObject Args { get; set; }

        public GameMessage(string gameId, Guid objectId, OperationType operationId, JsonObject args)
        {
            GameId = gameId;
            GameObjectId = objectId;
            OperationId = operationId;
            Args = args;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
