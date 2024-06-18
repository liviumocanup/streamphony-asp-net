using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.BlobStorage.Commands;
using Streamphony.Application.App.BlobStorage.DTOs;
using Streamphony.Application.Common.Enum;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/blobs")]
public class BlobController(IMediator mediator) : AppBaseController
{
    [HttpPost("image")]
    [ValidateModel]
    [ExtractUserId]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized();
        
        var blobRequestDto = new BlobRequestDto
        {
            FileName = file.FileName,
            Length = file.Length,
            ContentType = file.ContentType,
            Content = file.OpenReadStream()
        };

        var blobDto = await mediator.Send(new UploadBlob(blobRequestDto, userId, false));

        return CreatedAtAction(nameof(UploadImage), new { id = blobDto.Id }, blobDto);
    }
    
    [HttpPost("audio")]
    [ValidateModel]
    [ExtractUserId]
    public async Task<IActionResult> UploadAudio(IFormFile file)
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized();
        
        var blobRequestDto = new BlobRequestDto
        {
            FileName = file.FileName,
            Length = file.Length,
            ContentType = file.ContentType,
            Content = file.OpenReadStream()
        };

        var blobDto = await mediator.Send(new UploadBlob(blobRequestDto, userId, true));

        return CreatedAtAction(nameof(UploadAudio), new { id = blobDto.Id }, blobDto);
    }

    [HttpDelete("{id:guid}")]
    [ExtractUserId]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBlob(Guid id)
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized();
        
        await mediator.Send(new DeleteBlob(id, userId));
        return NoContent();
    }
    
    [HttpPut("commit")]
    [ExtractUserId]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CommitBlob(Guid blobId, BlobType blobType, CancellationToken cancellationToken)
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized();
        
        await mediator.Send(new CommitBlob(blobId, userId, blobType), cancellationToken: cancellationToken);
        return NoContent();
    }
}
