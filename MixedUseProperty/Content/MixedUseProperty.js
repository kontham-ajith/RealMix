var myMastUrl = {};
$(document).ready(function () {
    GetURL();
    $('#btnAddProperty').click(function (e) {
        if ($("#txtPropName").val().trim() == "") {
            $('#txtPropName').attr("disabled", false);
            alert("Property Name Required");
            return false;
        } else {
            var PropName = $("#txtPropName").val().trim();
            var strpropcode = $("#txtPropCode").val().trim();
            var strProprep = $("#txtProprep").val().trim();
            var strEmail = $("#txtEmail").val().trim();
            var ProperyData = {
                strPropName: PropName,
                strpropcode: strpropcode,
                strProprep: strProprep,
                strEmail: strEmail,
                strPropAddress: $("#txtPropAddress").val().trim()
            };
            $.ajax({
                url: myMastUrl.AddProperty,
                data: JSON.stringify(ProperyData),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('#btnAdd').attr("disabled", false);
                    if (data.message == "Property Added") {
                        alert("Property Added Successfully.");
                        $(location).attr('href', '/Property');
                    } else {
                        alert(data.message);
                        return false;
                    }
                },
                error: function (xhr, er, error) {
                    $('#btnAdd').attr("disabled", false);
                    if (err == "") {
                        alert("Property Added Successfully.");
                        $(location).attr('href', '/Property');
                    }
                }
            });
        }
    });

    $('#btnAddSpace').click(function (e) {
        if ($("#txtSpaceName").val().trim() == "") {
            $('#txtSpaceName').attr("disabled", false);
            alert("Space Name Required");
            return false;
        } else {
            var ProperyData = {
                strSpace: $("#txtSpaceName").val().trim(),

            };
            $.ajax({
                url: myMastUrl.AddSpace,
                data: JSON.stringify(ProperyData),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('#btnAdd').attr("disabled", false);
                    if (data.message == "Space Added") {
                        alert("Space Added Successfully.");
                        $(location).attr('href', '/Spaces');
                    } else {
                        alert(data.message);
                        return false;
                    }
                },
                error: function (xhr, er, error) {
                    $('#btnAddSpace').attr("disabled", false);
                    if (err == "") {
                        alert("Space Added Successfully.");
                        $(location).attr('href', '/Property');
                    }
                }
            });

        }
    });
    $('#btnAddSite').click(function (e) {
        if ($("#txtSiteName").val().trim() == "") {
            $('#txtSiteName').attr("disabled", false);
            alert("Site Name Required");
            return false;
        } else {
            var ProperyData = {
                strSiteProID: $("#ddlProperty").val(),
                strsiteName: $("#txtSiteName").val().trim(),
                strSiteCode: $("#txtSiteCode").val().trim(),
                strSiteNoofFloors: $("#txtNoofFloors").val().trim(),
                strsiteRepName: $("#txtsiteRepName").val().trim(),
                strsiteRepEmail: $("#txtsiteRepEmail").val().trim(),
                strSiteAddress: $("#siteAddress").val().trim()
            };
            $.ajax({
                url: myMastUrl.AddSite,
                data: JSON.stringify(ProperyData),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('#btnAddSite').attr("disabled", false);
                    $(location).attr('href', '/sites');
                    if (data.message == "Site Added") {
                        alert("Site Added Successfully.");
                        $(location).attr('href', '/sites');
                    } else {
                        alert(data.message);
                        return false;
                    }
                },
                error: function (xhr, er, error) {
                    $('#btnAdd').attr("disabled", false);
                    if (err == "") {
                        alert("Site Added Successfully.");
                    }
                }
            });
        }
    })
});