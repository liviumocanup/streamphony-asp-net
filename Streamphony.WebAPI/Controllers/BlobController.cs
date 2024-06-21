using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streamphony.Application.App.BlobStorage.Commands;
using Streamphony.Application.App.BlobStorage.DTOs;
using Streamphony.WebAPI.Filters;

namespace Streamphony.WebAPI.Controllers;

[Route("api/blobs")]
public class BlobController(IMediator mediator) : AppBaseController
{
    [HttpPost]
    [ValidateModel]
    [ExtractUserId]
    public async Task<IActionResult> UploadBlob(IFormFile file, [FromForm] string blobType, CancellationToken cancellationToken)
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

        var blobDto = await mediator.Send(new UploadBlob(blobRequestDto, userId, blobType), cancellationToken);

        return CreatedAtAction(nameof(UploadBlob), new { id = blobDto.Id }, blobDto);
    }

    [HttpDelete("{id:guid}")]
    [ExtractUserId]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBlob(Guid id, CancellationToken cancellationToken)
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized();
        
        await mediator.Send(new DeleteBlob(id, userId), cancellationToken);
        return NoContent();
    }
    
    [HttpPut("commit")]
    [ExtractUserId]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CommitBlob(Guid blobId, Guid relatedEntityId, string blobType, CancellationToken cancellationToken)
    {
        if (HttpContext.Items["UserId"] is not Guid userId)
            return Unauthorized();
        
        await mediator.Send(new CommitBlob(blobId, userId, relatedEntityId, blobType), cancellationToken);
        return NoContent();
    }
}
