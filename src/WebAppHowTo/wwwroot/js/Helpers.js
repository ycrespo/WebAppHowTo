function GetTextAreaInnerHtml(embedId, id)
{
    const embed = document.getElementById(embedId);
    var res = embed.contentWindow.document.getElementById(id).value;
    return res;
}