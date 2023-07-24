import { Component, OnInit, OnDestroy, AfterViewInit } from "@angular/core";
import { NavigationService } from "../../../shared/services/navigation.service";
import { ThemeService } from "../../services/theme.service";
import { Subscription } from "rxjs";
import { ILayoutConf, LayoutService } from "app/shared/services/layout.service";
import { JwtAuthService } from "app/shared/services/auth/jwt-auth.service";
import { NestedTreeControl } from "@angular/cdk/tree";
import { MatTreeNestedDataSource } from "@angular/material/tree";
import { ActivatedRoute } from "@angular/router";



interface Reports {
  name?: string;
  name1?: string;
  name2?: string;
  name3?: string;
  tooltip?: string;
  state?: string; // Router state
  icon?: string;  // Material icon name
  children?: Reports[];
}

const TREE_DATA: Reports[] = [
 
  {
    name: 'Master',
    children: [
      {
        name: 'Department',
        
      },
      {
        name1: 'Employs'
      },
      {
        name2: 'LeaveManagement',
        children:[
          {
            name3:'TakeLeave'
          }
        ]
      }
    ],
  },
];

@Component({
  selector: "app-sidebar-side",
  templateUrl: "./sidebar-side.component.html",
  styleUrls: ['./sidebar-side.css']
})
export class SidebarSideComponent implements OnInit, OnDestroy, AfterViewInit {

  treeControl = new NestedTreeControl<Reports>(node => node.children);
  dataSource = new MatTreeNestedDataSource<Reports>();

  hasChild = (_: number, node: Reports) => !!node.children && node.children.length > 0;
  
  public menuItems: any[];
  public hasIconTypeMenuItem: boolean;
  public iconTypeMenuTitle: string;
  private menuItemsSub: Subscription;
  public layoutConf: ILayoutConf;

  constructor(
    private navService: NavigationService,
    public themeService: ThemeService,
    private layout: LayoutService,
    public jwtAuth: JwtAuthService,
    private route: ActivatedRoute,
  ) {
    this.dataSource.data = TREE_DATA;
  }

  ngOnInit() {
    this.iconTypeMenuTitle = this.navService.iconTypeMenuTitle;
    this.menuItemsSub = this.navService.menuItems$.subscribe(menuItem => {
      this.menuItems = menuItem;
      //Checks item list has any icon type.
      this.hasIconTypeMenuItem = !!this.menuItems.filter(
        item => item.type === "icon"
      ).length;
    });
    this.layoutConf = this.layout.layoutConf;
  }
  ngAfterViewInit() {}
  ngOnDestroy() {
    if (this.menuItemsSub) {
      this.menuItemsSub.unsubscribe();
    }
  }
  toggleCollapse() {
    if (
      this.layoutConf.sidebarCompactToggle
    ) {
        this.layout.publishLayoutChange({
        sidebarCompactToggle: false
      });
    } else {
        this.layout.publishLayoutChange({
            // sidebarStyle: "compact",
            sidebarCompactToggle: true
          });
    }
  }
}
