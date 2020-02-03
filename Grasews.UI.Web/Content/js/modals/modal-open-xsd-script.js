//var externalSchemasNotFound;
//var externalSchemasNotFoundIndex = 0;
//var numberOfExternalSchemasNotFound = 0;

//$(function () {
//    $("#divOpenXsdByFile").hide();

//    $("#radioOpenXsdByURL").on("click", function () {
//        $("#txtXsdUrl").prop('disabled', false);
//        $("#fileOpenXsd").prop('disabled', true);

//        $("#divOpenXsdByUrl").show();
//        $("#divOpenXsdByFile").hide();
//    });

//    $("#radioOpenXsdByFile").on("click", function () {
//        $("#txtXsdUrl").prop('disabled', true);
//        $("#fileOpenXsd").prop('disabled', false);

//        $("#divOpenXsdByUrl").hide();
//        $("#divOpenXsdByFile").show();
//    });

//    $("#lnkApplyOpenXsd").on("click", function () {
//        openXSD();
//    });
//});

//function openXSD() {
//    jQuery.support.cors = true;

//    if ($("#radioOpenXsdByFile").is(':checked')) {
//        var myFile = $('#fileOpenXsd').prop('files');

//        if (myFile.length > 0) {
//            $("#formOpenXsdByFile").submit();
//        }
//    }
//    else {
//        var includeSchemaAsyncMethod = "Cytoscape/IncludeSchemaAsync";

//        var txtExternalXsdFiles = $(".xsd-external-file");
//        var newExternalXsdPaths = [];

//        txtExternalXsdFiles.each(function (id, element) {
//            newExternalXsdPaths[id] = $(element).val();
//        });

//        $.ajax({
//            url: url + includeSchemaAsyncMethod,
//            data: {
//                "NewExternalXsdPaths": newExternalXsdPaths,
//                "ExternalXsdPathsNotFound": externalSchemasNotFound,
//                "WsdlPath": cytoscapeServiceResult.wsdlDocument.WsdlPath
//            },
//            type: 'POST',
//            crossDomain: true,
//            success: function (includeSchemaAsyncMethodResponse) {
//                serviceDescriptionNameOnScreen = includeSchemaAsyncMethodResponse.wsdlDocument.ServiceName;
//                fillWebServiceTreeView(includeSchemaAsyncMethodResponse.wsdlDocument);
//                fillCytoscape(includeSchemaAsyncMethodResponse.cytoscape);
//                fillCodeMirror(includeSchemaAsyncMethodResponse.wsdlDocument.WsdlRaw);
//                saveWebServiceInSession(includeSchemaAsyncMethodResponse.wsdlDocument);
//            },
//            error: function (xhr, textStatus, errorThrown) {
//                addToastMessage("ERROR!\n" + xhr + "\n" + textStatus + "\n" + errorThrown, "danger");
//            }
//        });
//    }
//}