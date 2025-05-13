using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Common;

namespace WebAPI.Entities;

public class EditableContent : BaseEntity
{
    public string Page { get; set; }
    public string Key { get; set; }
    public string Language { get; set; }
    public string Content { get; set; }

    public int LastEditorId { get; set; }
    public User LastEditor { get; set; }
}