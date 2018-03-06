import { Component, OnInit, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import '../../../../app.module.client';
import { TREE_MAXNODESTOEXPAND,CUSTOMIZETABLE } from '../../../../shared/global.constant';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TreeNode, Dropdown, Button, SelectItem, AutoComplete } from 'primeng/primeng';
import { CommonModule } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { PetroTable, Updates } from '../../../../models/petro/petroTable.model';
import { ViewEncapsulation } from '@angular/core';
import { XmlService } from '../../../../services/petro/Xml.service';
import {LoadingComponent } from '../../loading.component';
@Component({
    selector: 'tabletree',
    templateUrl: './tabletree.component.html',
    styleUrls: ['./tabletree.component.css'],
    providers: [XmlService],
    encapsulation: ViewEncapsulation.None,
})

export class TableTreeComponent implements OnInit, OnChanges {
    @Input() selectedTable: PetroTable;
    treenode: TreeNode[];
    filterNodes: TreeNode[];
    selectedNode: TreeNode;
    displayUpdateTreeDialog: boolean = false;
    newValue: string;
    updates: Updates[] = [];
    filterValue: string;
    filters: string[] = [];

    filterKey: string;
    filterKeys: string[] = [];

    loading: boolean = false;
    timeout: number = 100;
    constructor(private xmlService: XmlService) { }

    ngOnInit() {     
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes['selectedTable']) {
            this.filterValue = '';
            this.filters = [];
            this.loadTree();
        }
    }

    nodeSelect(event) {
        this.selectedNode = event.node;
        if (this.selectedNode.children == null) {           
            if (this.selectedNode.data && (this.selectedNode.data.isComment as boolean === false)) {
                this.newValue = this.selectedNode.label;
                this.displayUpdateTreeDialog = true;
            }
        } else if (this.selectedNode.children && this.selectedNode.children[0].children == null) {
            this.filterKey = this.selectedNode.label;
            this.filterData();
        } else if (!this.selectedNode.parent) {
            this.reloadTree();
        }
    }

    updateDefaultXmlValue() {
        if (this.selectedNode.label != this.newValue) {           
            this.updateLeaf(this.selectedNode);
            this.selectedNode.data.newValue = this.newValue;
            let updated: Updates = {
                oldValue: this.selectedNode.data.oldValue,
                rowNum: this.selectedNode.data.rowNum,
                colName: this.selectedNode.data.colName,
                newValue: this.newValue
            };
            this.selectedNode.label = this.newValue;
            this.updates.push(updated);
        }
        this.displayUpdateTreeDialog = false;
    }

    expandAll(nodes: TreeNode[]) {
        this.loading = true;
        this.rowNum = 0;
        if (nodes != undefined) {
            nodes.forEach(node => {
                this.expandRecursive(node, true);
            });
        }
        this.loading = false;
    }
    rowNum: number = 0;
    private expandRecursive(node: TreeNode, isExpand: boolean) {
        try {
            if (node.children) {
                node.expanded = isExpand;
                this.rowNum++;
                  
                if (node.children) {
                    node.children.forEach(childNode => {
                        if (this.rowNum < TREE_MAXNODESTOEXPAND) {
                            this.expandRecursive(childNode, isExpand);
                        }
                    });
                }                
            }
            else {               
                if (node.data && (node.data.isComment as boolean === true)) {                  
                    this.commentLeaf(node);
                }
            }
        } catch (Error) {
            console.log(Error);
        }
    }

    loadTree() {   
        if (this.selectedTable) {
            this.filterNodes = [];
            this.filterKeys = [];
            this.filterKey = undefined;
            this.filterValue = undefined;
            this.filters = [];
            if (this.selectedTable.tableName.toLowerCase() === CUSTOMIZETABLE.toLowerCase()) {
                this.timeout = 1000;
            }           
            if (this.selectedTable.defaultXML) {
                this.loading = true;              
                this.xmlService.getTreeFromDefaultXml(this.selectedTable.defaultXML)
                    .then(nodes => {
                        this.treenode = nodes.data;                      
                        this.getFilters();
                        this.filterNodes = nodes.data;
                        this.expandAll(this.filterNodes);
                        this.loading = false;                       
                    },
                    (error) => {
                        console.log(error);
                        this.loading = false;
                    }
                    );
            }
        }
    }
       

    getFilters() {
        this.treenode[0].children.forEach(
            c => {
                this.getFilter(c);
            }
        );
    }

    getFilter(node: TreeNode) {       
        if (node.children) {
            node.children.forEach(c => {
                if (c.children != null) {
                    this.getFilter(c);
                } else {
                    if (this.filters.indexOf(node.label) < 0) {                       
                        if (c.data && c.data.isComment as boolean == false) {
                            this.filters.push(node.label);                        
                        }
                    }
                }
            });
        }     
    }

    filterData() {
        if (this.filterKey) {
            let nodes: TreeNode[] = [];
            this.treenode[0].children.forEach(
                c => {
                    this.filterNodeData(c, nodes);
                }
            );

            if (nodes.length > 0) {
                this.filterNodes = [];
                var root: TreeNode = { label: this.treenode[0].label };
                this.filterNodes = [root];
                this.filterNodes[0].children = [];
                nodes.forEach(node => {
                    if (node.parent) {
                        var rowNode = this.rowNode(node, this.filterNodes[0].label)
                        if (rowNode) {
                            this.filterNodes[0].children.push(rowNode);
                        }
                    }
                });             
                this.expandAll(this.filterNodes);
            } else {
                this.reloadTree();
            }
        }
    }

    rowNode(node: TreeNode, rootLable): TreeNode {
        if (!node || !node.parent) {
            return undefined;
        } else
            if (node.parent && node.parent.label == rootLable) {
           
                return node;
             
        } else {
            return this.rowNode(node.parent, rootLable);
        }
    }

    filterNodeData(node: TreeNode, nodes: TreeNode[]) {    
        if (node.children) {
            node.children.forEach(c => {
                if (!c.children) {
                    if (c.data && c.data.isComment as boolean == false) {
                        this.resetHighligt(c);
                        if (node.label === this.filterKey) {
                            if (this.filterValue) {
                                if (c.label.toLowerCase().startsWith(this.filterValue.toLowerCase())) {
                                    nodes.push(c);
                                    this.highlight(c);
                                }

                            } else {
                                if (!c.label) {
                                    nodes.push(c);
                                    this.highlight(c);
                                }
                            }
                        }
                    }
                } else {
                    this.filterNodeData(c, nodes);
                }
            });
        }
    }


    highlight(node: TreeNode) {
        node.styleClass = 'HighlightLeaf';      
    }

    setLeafStyle(node: TreeNode) {
        node.styleClass = 'Leaf';
        node.icon = 'fa fa-pencil-square-o';
    }

    updateLeaf(node: TreeNode) {        
        node.styleClass = 'UpdatedLeaf';     
        node.icon = 'fa fa-check';      
    }
    commentLeaf(node: TreeNode) {
        node.styleClass = 'CommentLeaf';
    }

    resetHighligt(node: TreeNode) {  
        if (node.styleClass) {
            if (node.data.isComment as boolean === false) {  
                if (!node.data.newValue) {
                    node.styleClass = undefined;     
                    node.icon = 'fa fa-pencil-square-o';
                } else {
                    this.updateLeaf(node);
                }                
            }
        }
    }

    reloadTree() {
        this.filterNodes = this.treenode;
        this.filters = [];
        this.getFilters();
        this.expandAll(this.filterNodes);
    }

    search(event) {     
        this.filterKeys = [];
        this.filters.forEach(f => {
            if (f.toLowerCase().startsWith(event.query.toLowerCase())) {
                this.filterKeys.push(f)
            }
        }
        );     
    }

    change(event) {
        this.filterValue = event;
        setTimeout(() => {
            this.filterData();
        }, this.timeout);
    }

    mousedown(event) {       
        if (!this.filterValue) {
            this.filterData();
        }
    }
}