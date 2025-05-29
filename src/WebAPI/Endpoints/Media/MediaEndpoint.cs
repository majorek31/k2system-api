using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Extensions;
using WebAPI.Features.Media.Commands;
using WebAPI.Features.Media.Commands.DeleteFile;
using WebAPI.Features.Media.Queries.GetAllMedia;
using WebAPI.Features.Media.Queries.GetMediaById;

namespace WebAPI.Endpoints.Media;

public class MediaEndpoint : Endpoint
 {
     public override void MapEndpoints(IEndpointRouteBuilder route)
     {
         var group = route.MapGroup("media");

         Configure<UploadFileDto, Unit>(
             group.MapPost("", async (IMediator mediator, [FromForm] UploadFileDto dto) =>
                 {
                     var result = await mediator.Send(new UploadFileCommand(dto.File));
                     return Results.Created(result.Path, new());
                 })
                 .RequireScope("write:content")
                 .DisableAntiforgery()
                 .Accepts<UploadFileDto>("multipart/form-data"),
             "UploadFile",
             "Media",
             "Uploads file to the server that can be used in other parts of application"
         );
         Configure<IEnumerable<MediaDto>>(
             group.MapGet("", async (IMediator mediator) =>
             {
                 var result = await mediator.Send(new GetAllMediaQuery());
                 return Results.Ok(result);
             })
              .RequireScope("read:content"),
             "GetMedia",
             "Media",
             "Returns media from the server"
         );
         Configure<Unit>(
             group.MapDelete("/{mediaId:int}", async (IMediator mediator, int mediaId) =>
             {
                 await mediator.Send(new DeleteFileCommand(mediaId));
                 return Results.Ok();
             }).RequireScope("write:content"),
             "DeleteMedia",
             "Media",
             "Deletes media from the server"
         );
         Configure<MediaDto>(
             group.MapGet("/{mediaId:int}", async (IMediator mediator, int mediaId) =>
             {
                 var result = await mediator.Send(new GetMediaByIdQuery(mediaId));
                 return result is null ? Results.NotFound() : Results.Ok(result);
             }).RequireScope("read:content"),
             "GetMediaById",
             "Media",
             "Returns media from server with specified id"
         );
     }
 }