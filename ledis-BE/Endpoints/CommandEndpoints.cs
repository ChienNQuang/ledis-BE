using ledis_BE.Commands;
using ledis_BE.Models;
using ledis_BE.Resp;
using Microsoft.AspNetCore.Mvc;

namespace ledis_BE.Endpoints;

public static class CommandEndpoints
{
    public static void MapCommandEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/commands", (
            [FromBody] ProcessCommandRequest request,
            [FromServices] DataStore dataStore) =>
        {
            RespValue result = CommandProcessor.Process(dataStore, request.Command, request.Arguments);

            return Results.Ok(result.GetValue());
        });

        app.MapGet("/commands/save", ([FromServices] DataStore dataStore) =>
        {
            Stream fileStream = CommandProcessor.SaveSnapshot(dataStore);
            
            if (fileStream.CanSeek)
                fileStream.Position = 0;

            string fileName = $"snapshot-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.json";
            
            return Results.File(fileStream, "application/json", fileName);
        });
    }
}

public class ProcessCommandRequest
{
    /// <summary>
    /// Command should be a string, case-insensitive
    /// </summary>
    public string Command { get; set; } = null!;
    /// <summary>
    /// Each arguments should be in base64 format, case-sensitive
    /// </summary>
    public string[] Arguments { get; set; } = [];
}