var cy;
var initialZoom;
var zoom;

function clearCytoscape() {
    cy.elements().remove();
}

function fillCytoscape(data) {
    cy = window.cy = cytoscape({
        wheelSensitivity: 0.50,

        container: document.getElementById('graph_container'),

        boxSelectionEnabled: false,
        autounselectify: true,

        layout: {
            name: 'dagre'
        },

        style: [
            {
                selector: 'node',
                style: {
                    'content': 'data(label)',
                    'color': '#000',
                    'text-valign': 'top',
                    'text-halign': 'left',
                    'text-outline-width': 0.0,
                    'text-outline-color': '#000',
                    'font-size': 20,
                    'width': 40,
                    'height': 20
                }
            },

            {
                selector: '.node',
                style: {
                    'border-style': 'solid',
                    'border-width': 2,
                    'border-color': '#000',
                    'border-opacity': 0.3
                }
            },
            //Ontology classes -----------------------------------------------------------------
            {
                selector: '.ontology-node',
                style: {
                    'shape': 'circle',

                    'background-opacity': 0.7,
                    'background-color': '#FF0000',

                    'width': 20,
                    'height': 20,

                    'border-style': 'solid',
                    'border-width': 2,
                    'border-color': '#000',
                    'border-opacity': 0.3
                }
            },
            //Group -----------------------------------------------------------------
            {
                selector: '.group',
                style: {
                    'text-valign': 'top',
                    'text-halign': 'center',
                    'text-margin-y': -5,
                    'font-size': 10
                }
            },

            {
                selector: ':parent',
                style: {
                    'background-color': '#ffffff'
                }
            },
            //WSDL -----------------------------------------------------------------
            {
                selector: '.node-wsdl-interface',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#4287f5', //dark blue
                    'shape': 'hexagon',
                    'width': 80,
                    'height': 40

                    //'background-image': 'https://live.staticflickr.com/7272/7633179468_3e19e45a0c_b.jpg',
                    //'background-image': 'content/img/issue_node.jpg',
                    //'background-image-opacity': 1,
                    //'background-fit': 'cover'
                }
            },

            {
                selector: '.node-wsdl-operation',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#00FFFF', //cyan
                    'shape': 'hexagon',
                    'width': 70,
                    'height': 35
                }
            },

            {
                selector: '.node-wsdl-input',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#00c954', //green
                    'shape': 'hexagon',
                    //'shape': 'polygon',
                    //'shape-polygon-points': '-1, -1,   1, -1,   1, 1,   -1, 1,   -0.5, 0',
                    'width': 50,
                    'height': 25
                }
            },

            {
                selector: '.node-wsdl-output',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#ff00a9', //pink
                    'shape': 'hexagon',
                    //'shape': 'polygon',
                    //'shape-polygon-points': '-1, -1,   1, -1,   0.5, 0,   1, 1,   -1, 1',
                    'width': 50,
                    'height': 25
                }
            },

            {
                selector: '.node-wsdl-infault',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#a52a2a', //brown
                    //'background-color': '#993299', //purple
                    'shape': 'hexagon',
                    'width': 50,
                    'height': 25
                }
            },

            {
                selector: '.node-wsdl-outfault',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#a52a2a', //brown
                    'shape': 'hexagon',
                    'width': 50,
                    'height': 25
                }
            },

            {
                selector: '.node-wsdl-fault',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#a52a2a', //brown
                    'shape': 'hexagon',
                    'width': 50,
                    'height': 25
                }
            },

            {
                selector: '.node-xsd-complex-type',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#c6c6c6', //gray
                    'shape': 'rectangle',
                    'width': 30,
                    'height': 30
                }
            },

            {
                selector: '.node-xsd-simple-type',
                style: {
                    'background-opacity': 0.3,
                    'background-color': '#ffa500', //orange
                    'shape': 'rectangle',
                    'width': 13,
                    'height': 13
                }
            },
            //WSDL Annotated -----------------------------------------------------------------
            {
                selector: '.node-wsdl-interface-annotated',
                style: {
                    'background-opacity': 1,
                    'background-color': '#4287f5', //dark blue
                    'shape': 'hexagon',
                    'width': 80,
                    'height': 40,
                    'border-opacity': 1,
                    'border-width': 4
                }
            },

            {
                selector: '.node-wsdl-operation-annotated',
                style: {
                    'background-opacity': 1,
                    'background-color': '#00FFFF', //cyan
                    'shape': 'hexagon',
                    'width': 70,
                    'height': 35,
                    'border-opacity': 1,
                    'border-width': 4
                }
            },

            {
                selector: '.node-xsd-complex-type-annotated',
                style: {
                    'background-opacity': 1,
                    'background-color': '#c6c6c6', //gray
                    'shape': 'rectangle',
                    'width': 30,
                    'height': 30,
                    'border-opacity': 1,
                    'border-width': 4
                }
            },

            {
                selector: '.node-xsd-simple-type-annotated',
                style: {
                    'background-opacity': 1,
                    'background-color': '#ffa500', //orange
                    'shape': 'rectangle',
                    'width': 13,
                    'height': 13,
                    'border-opacity': 1,
                    'border-width': 4
                }
            },
            //WSDL with Model Reference -----------------------------------------------------------------
            {
                selector: '.node-wsdl-interface-model-reference',
                style: {
                    'border-color': '#F00'
                }
            },

            {
                selector: '.node-wsdl-operation-model-reference',
                style: {
                    'border-color': '#F00'
                }
            },

            {
                selector: '.node-xsd-complex-type-model-reference',
                style: {
                    'border-color': '#F00'
                }
            },

            {
                selector: '.node-xsd-simple-type-model-reference',
                style: {
                    'border-color': '#F00'
                }
            },
            //WSDL with Schema Mapping -----------------------------------------------------------------
            {
                selector: '.node-xsd-complex-type-schema-mapping',
                style: {
                    'border-style': 'double'
                }
            },

            {
                selector: '.node-xsd-simple-type-schema-mapping',
                style: {
                    'border-style': 'double'
                }
            },

            {
                selector: 'edge',
                style: {
                    'width': 0.5
                }
            },
            //Edges -------------------------------------------------------------------------------
            {
                selector: 'edge.edge-service-description-nodes',
                style: {
                    'curve-style': 'bezier',
                    'line-color': '#000',
                    'source-arrow-color': '#000',
                    'source-arrow-shape': 'vee'
                }
            },

            {
                selector: 'edge.edge-model-reference-nodes',
                style: {
                    'curve-style': 'bezier',
                    'line-color': '#FF0000',
                    'line-style': 'dashed',
                    'edge-text-rotation': 'autorotate',
                    'font-size': '8px',
                    'color': '#333333'
                }
            },

            {
                selector: 'edge.edge-intermediate-ontology-term-nodes',
                style: {
                    'curve-style': 'bezier',
                    'line-color': '#C0C0C0',
                    'target-arrow-color': '#C0C0C0',
                    'target-arrow-shape': 'triangle',
                    'line-style': 'solid'
                }
            },
            //Issues -----------------------------------------------------------------
            {
                selector: '.node-with-issues',
                style: {
                    'background-color': '#FF0'
                }
            },
            //Others -----------------------------------------------------------------
            {
                selector: '.multiline-manual',
                style: {
                    'text-wrap': 'wrap'
                }
            }
        ],

        elements: data
    });

    cy = createContextMenusForGraph(cy);

    cy.on('tap', 'node', function (evt) {
        var parentNode = evt.cyTarget;
        //TODO: Need to fix
        //displayChildrenNodesFromParentNode(parentNode, null);
    });

    cy.on('tap', 'edge', function (evt) {
        var parentEdge = evt.cyTarget;
        //TODO: Need to fix
        //displayChildrenNodesFromParentEdge(parentEdge, null);
    });

    initialZoom = cy.zoom();
    zoom = initialZoom;

    resetGraphLayout();

    cy.on("zoom", function () {
        zoom = cy.zoom();
        var zoomValue = Math.floor(zoom / initialZoom * 100);
        $("#zoomValue").text(zoomValue + "%");
    });
}

function resetGraphLayout() {
    cy.fit();
    zoom = initialZoom;
    cy.zoom(zoom);

    $("#zoomValue").text("100%");

    cy.layout();
}

function adjustUserNodesPositionsOnGraph(data) {
    var originalJson = cy.json();

    $.each(data.nodes, function (index, node) {
        if (node.position) {
            var originalNode = findNode(node.data.id, originalJson.elements.nodes);
            originalNode.position = node.position;
        }
    });

    cy.json(originalJson);

    cy.fit();
}

var findNode = function (id, nodes) {
    for (var i = 0, len = nodes.length; i < len; i++) {
        if (nodes[i].data.id === id)
            return nodes[i]; // Return as soon as the object is found
    }
    return null; // The object was not found
};

$(function () {
    $(window).resize(function () {
        cy.resize();
        cy.fit();
    });

    $(".sidebar-toggle").click(function () {
        setTimeout(
            function () {
                cy.resize();
                cy.fit();
                initialZoom = cy.zoom();
                zoom = initialZoom;

                $("#zoomValue").text(zoom);
            }, 300);
    });

    //$("#btn_expand_all_nodes").click(function () {
    //});

    //$("#btn_collapse_all_nodes").click(function () {
    //});

    $("#btn_reset_graph_layout").click(function () {
        if (cy !== null) {
            resetGraphLayout();
        }
    });

    $("#btn_fit_graph_layout").click(function () {
        if (cy !== null && cy !== undefined) {
            cy.fit();
        }
    });

    $("#btn_graph_zoom_in").click(function () {
        if (cy !== null) {
            cy.zoom(cy.zoom() + 0.10);
        }
    });

    $("#btn_graph_zoom_out").click(function () {
        if (cy !== null) {
            cy.zoom(cy.zoom() - 0.10);
        }
    });

    $('.nav-tabs a').on('shown.bs.tab', function () {
        if ($(event.target).context.hash === "#tab_design") {
            if (cy !== null) {
                cy.resize();
                cy.fit();
            }
        }
    });
});

//TODO: Check show var already declared
function displayChildrenNodesFromParentNode(node, show) {
    var edges = node.connectedEdges();
    show = $(node).attr("has-hidden-child-node") !== "true";
    var parentIsVisible = node.style("display") !== "element";

    for (var i = 0; i < edges.length; i++) {
        if (edges[i].source() !== node) {
            var childNode = edges[i].target();

            if (parentIsVisible && show) {
                childNode.style("display", "element");
            } else {
                if (parentIsVisible && /*childNode.style("display") !== "none" &&*/ edges[i].source().style("display") !== "element") {
                    childNode.style("display", "element");
                    show = true;
                }
                else {
                    childNode.style("display", "none");
                    show = false;
                }
            }

            displayChildrenNodesFromParentNode(childNode, show);
        }
    }

    $(node).attr("has-hidden-child-node", "false");
}

function displayChildrenNodesFromParentEdge(edge) {
    var node = edge.target();
    var edges = node.connectedEdges();
    var show;

    if (node.style("display") !== "none") {
        node.style("display", "element");
    } else {
        node.style("display", "none");
    }

    var parentIsVisible = node.style("display") !== "element";

    for (var i = 0; i < edges.length; i++) {
        if (edges[i].source() !== node) {
            var childNode = edges[i].target();

            if (!parentIsVisible) {
                if (!childNode.hasClass("ola")) {
                    childNode.style("display", "none");
                }
                show = false;
            }
            else {
                if (childNode.style("display") !== "none" && edges[i].source().style("display") !== "element") {
                    childNode.style("display", "element");
                    show = true;
                }
                else {
                    if (!childNode.hasClass("ola")) {
                        childNode.style("display", "none");
                    }
                    show = false;
                }
            }

            displayChildrenNodesFromParentNode(childNode, show);
        }
        if (node.style("display") !== "none") {
            $(edges[i].source()).attr("has-hidden-child-node", "false");
        } else {
            $(edges[i].source()).attr("has-hidden-child-node", "true");
        }
    }
}

//Get properties from the cytoscape node element and open the modal for ADDING the MODEL REFERENCE
function cytoscapeContextMenuAddModelReference(ele) {
    var wsdlElementName = ele.data('name');
    var wsdlElementType = ele.data('node-type');
    var idWsdlElement = ele.data('id-wsdl-element');
    var idWsdlElementType = ele.data('id-node-type');
    var idServiceDescription = ele.data('id-service-description');

    modalAddModelReference(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
}

//Get properties from the cytoscape node element and open the modal for REMOVING the MODEL REFERENCE
function cytoscapeContextMenuRemoveModelReference(ele) {
    var wsdlElementName = ele.data('name');
    var wsdlElementType = ele.data('node-type');
    var idWsdlElement = ele.data('id-wsdl-element');
    var idWsdlElementType = ele.data('id-node-type');
    var idServiceDescription = ele.data('id-service-description');

    modalRemoveModelReference(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
}

//Get properties from the cytoscape node element and open the modal for ADDING the LIFTING SCHEMA MAPPING
function cytoscapeContextMenuAddLiftingSchemaMapping(ele) {
    var wsdlElementName = ele.data('name');
    var wsdlElementType = ele.data('node-type');
    var idWsdlElement = ele.data('id-wsdl-element');
    var idWsdlElementType = ele.data('id-node-type');
    var idServiceDescription = ele.data('id-service-description');

    modalAddLiftingSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
}

//Get properties from the cytoscape node element and open the modal for REMOVING the LIFTING SCHEMA MAPPING
function cytoscapeContextMenuRemoveLiftingSchemaMapping(ele) {
    var wsdlElementName = ele.data('name');
    var wsdlElementType = ele.data('node-type');
    var idWsdlElement = ele.data('id-wsdl-element');
    var idWsdlElementType = ele.data('id-node-type');
    var idServiceDescription = ele.data('id-service-description');

    modalRemoveLiftingSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
}

//Get properties from the cytoscape node element and open the modal for ADDING the LOWERING SCHEMA MAPPING
function cytoscapeContextMenuAddLoweringSchemaMapping(ele) {
    var wsdlElementName = ele.data('name');
    var wsdlElementType = ele.data('node-type');
    var idWsdlElement = ele.data('id-wsdl-element');
    var idWsdlElementType = ele.data('id-node-type');
    var idServiceDescription = ele.data('id-service-description');

    modalAddLoweringSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
}

//Get properties from the cytoscape node element and open the modal for REMOVING the LOWERING SCHEMA MAPPING
function cytoscapeContextMenuRemoveLoweringSchemaMapping(ele) {
    var wsdlElementName = ele.data('name');
    var wsdlElementType = ele.data('node-type');
    var idWsdlElement = ele.data('id-wsdl-element');
    var idWsdlElementType = ele.data('id-node-type');
    var idServiceDescription = ele.data('id-service-description');

    modalRemoveLoweringSchemaMapping(idWsdlElement, idWsdlElementType, wsdlElementType, wsdlElementName, idServiceDescription);
}

//Context menu for Model Reference and Schema Mapping, but with no annotations yet
function createContextMenuForModelReferenceAndSchemaMappingWithNoAnnotations(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-and-schema-mapping',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Add Lifting Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuAddLiftingSchemaMapping(ele);
                }
            },

            {
                content: 'Add Lowering Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuAddLoweringSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Lowering Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Lifting Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Model Reference',
                disabled: true
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference and Schema Mapping, but already with model reference annotations
function createContextMenuForModelReferenceAndSchemaMappingWithModelReferenceAnnotated(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-and-schema-mapping-with-model-reference-annotated',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Add Lifting Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuAddLiftingSchemaMapping(ele);
                }
            },

            {
                content: 'Add Lowering Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuAddLoweringSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Lowering Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Lifting Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuRemoveModelReference(ele);
                }
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference and Schema Mapping, but already with lifting schema mapping annotations
function createContextMenuForModelReferenceAndSchemaMappingWithLiftingSchemaMappingAnnotated(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-and-schema-mapping-with-lifting-annotated',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Add Lifting Schema Mapping',
                disabled: true
            },

            {
                content: 'Add Lowering Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuAddLoweringSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Lowering Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Lifting Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuRemoveLiftingSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Model Reference',
                disabled: true
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference and Schema Mapping, but already with lowering schema mapping annotations
function createContextMenuForModelReferenceAndSchemaMappingWithLoweringSchemaMappingAnnotated(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-and-schema-mapping-with-lowering-annotated',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Add Lifting Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuAddLiftingSchemaMapping(ele);
                }
            },

            {
                content: 'Add Lowering Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Lowering Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuRemoveLoweringSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Lifting Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Model Reference',
                disabled: true
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference and Schema Mapping, but already with lifting and lowering schema mapping annotations
function createContextMenuForModelReferenceAndSchemaMappingWithLiftingAndLoweringSchemaMappingAnnotated(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-and-schema-mapping-with-lifting-and-lowering-annotated',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Add Lifting Schema Mapping',
                disabled: true
            },

            {
                content: 'Add Lowering Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Lowering Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuRemoveLoweringSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Lifting Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuRemoveLiftingSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Model Reference',
                disabled: true
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference and Schema Mapping, but already with lifting schema mapping and model reference annotations
function createContextMenuForModelReferenceAndSchemaMappingWithLiftingSchemaMappingAndModelReferenceAnnotated(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-and-schema-mapping-with-lifting-and-model-reference-annotated',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Add Lifting Schema Mapping',
                disabled: true
            },

            {
                content: 'Add Lowering Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuAddLoweringSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Lowering Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Lifting Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuRemoveLiftingSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuRemoveModelReference(ele);
                }
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference and Schema Mapping, but already with lowering schema mapping and model reference annotations
function createContextMenuForModelReferenceAndSchemaMappingWithLoweringSchemaMappingAndModelReferenceAnnotated(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-and-schema-mapping-with-lowering-and-model-reference-annotated',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Add Lifting Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuAddLiftingSchemaMapping(ele);
                }
            },

            {
                content: 'Add Lowering Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Lowering Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuRemoveLoweringSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Lifting Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuRemoveModelReference(ele);
                }
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference and Schema Mapping, but with lifting and lowering schema mapping and model reference annotations already
function createContextMenuForModelReferenceAndSchemaMappingWithAllAnnotations(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-and-schema-mapping-with-lifting-and-lowering-and-model-reference-annotated',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Add Lifting Schema Mapping',
                disabled: true
            },

            {
                content: 'Add Lowering Schema Mapping',
                disabled: true
            },

            {
                content: 'Remove Lowering Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuRemoveLoweringSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Lifting Schema Mapping',
                select: function (ele) {
                    cytoscapeContextMenuRemoveLiftingSchemaMapping(ele);
                }
            },

            {
                content: 'Remove Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuRemoveModelReference(ele);
                }
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference only, but with no annotations yet
function createContextMenuForModelReferenceWithNoAnnotations(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-only',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Remove Model Reference',
                disabled: true
            }
        ]
    });

    return cy;
}

//Context menu for Model Reference only, but already with model reference annotations
function createContextMenuForModelReferenceWithModelReferenceAnnotated(cy) {
    cy.cxtmenu({
        selector: '.node-model-reference-only-with-model-reference-annotated',

        commands: [
            {
                content: 'Add Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuAddModelReference(ele);
                }
            },

            {
                content: 'Remove Model Reference',
                select: function (ele) {
                    cytoscapeContextMenuRemoveModelReference(ele);
                }
            }
        ]
    });

    return cy;
}

//Create the CONTEXT MENUs for the Cytoscape nodes
function createContextMenusForGraph(cy) {
    //For complex-elements, elements, and simple-elements
    cy = createContextMenuForModelReferenceAndSchemaMappingWithNoAnnotations(cy);

    //For complex-elements, elements, and simple-elements
    cy = createContextMenuForModelReferenceAndSchemaMappingWithModelReferenceAnnotated(cy);

    //For complex-elements, elements, and simple-elements
    cy = createContextMenuForModelReferenceAndSchemaMappingWithLiftingSchemaMappingAnnotated(cy);

    //For complex-elements, elements, and simple-elements
    cy = createContextMenuForModelReferenceAndSchemaMappingWithLoweringSchemaMappingAnnotated(cy);

    //For complex-elements, elements, and simple-elements
    cy = createContextMenuForModelReferenceAndSchemaMappingWithLiftingAndLoweringSchemaMappingAnnotated(cy);

    //For complex-elements, elements, and simple-elements
    cy = createContextMenuForModelReferenceAndSchemaMappingWithLiftingSchemaMappingAndModelReferenceAnnotated(cy);

    //For complex-elements, elements, and simple-elements
    cy = createContextMenuForModelReferenceAndSchemaMappingWithLoweringSchemaMappingAndModelReferenceAnnotated(cy);

    //For complex-elements, elements, and simple-elements
    cy = createContextMenuForModelReferenceAndSchemaMappingWithAllAnnotations(cy);

    //For interfaces, operations, infauls, and outfaults
    cy = createContextMenuForModelReferenceWithNoAnnotations(cy);

    //For interfaces, operations, infauls, and outfaults
    cy = createContextMenuForModelReferenceWithModelReferenceAnnotated(cy);

    return cy;
}