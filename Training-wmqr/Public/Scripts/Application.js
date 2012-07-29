function renderDocument(document) {

    var html = "<tr class='" + document.Id + "'>";
    html += "<td class='document-text'>" + document.Text + "</td>";
    html += "<td><a class='edit' href='#'>Edit</a></td>";
    html += "<td><a class='details' href='#'>Details</a></td>";
    html += "<td><a class='delete' href='#'>Delete</a></td>";
    html += "</tr>";
    return html;
}

function editDocument(elemnt) {
    var currentText =$(elemnt.closest('tr').find(".document-text")).html();
    var html = "<td colspan='3'><input class='new' type='text'/><input type='hidden' class='current' value='" + currentText + "'/></td>";
    html += "<td><input class='save' type='submit' value='save'/></td>";
    html += "<td><a class='cancel' href='#'>Cancel</a></td>";
    elemnt.closest('tr').html(html);
}

function cancelEdit(element) {
    var tr = element.closest('tr');
    var id = tr.attr('class');
    var text = tr.find(".current").val();
    tr.html($(renderDocument({ Id: id, Text: text })).html());
}

function deleteDocuemnt(element) {
    var id = element.closest('tr').attr('class');
    $.ajax({
        url: '/document/delete/' + id,
        type: "POST",
        success: function () {
            $("#documents ." + id).remove();
        }
    });
}

function saveEditDocuemnt(element) {
    var tr = element.closest('tr');
    var id = tr.attr('class');
    var text = tr.find('.new').val();
    $.ajax({
        url: '/document/edit/' + id,
        type: "POST",
        data: { Text: text },
        success: function () {
            tr.html($(renderDocument({ Id: id, Text: text })).html());
        }
    });
}