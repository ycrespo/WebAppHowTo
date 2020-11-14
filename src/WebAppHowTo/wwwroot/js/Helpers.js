function GetTextAreaInnerHtml(embedId, id)
{
    const embed = document.getElementById(embedId);
    return embed.contentWindow.document.getElementById(id).value;
}

function GetInputValue(inputId)
{
    return document.getElementById(inputId).value;
}

function SetReadOnlyOnOff(tagId)
{
    if (document.getElementById(tagId).hasAttribute("readonly"))
        document.getElementById(tagId).removeAttribute("readonly");
    else
        document.getElementById(tagId).setAttribute("readonly", true);
}

function SetHiddenOnOff(tagId)
{
    if (document.getElementById(tagId).hasAttribute("hidden"))
        document.getElementById(tagId).removeAttribute("hidden");
    else
        document.getElementById(tagId).setAttribute("hidden", true);
}