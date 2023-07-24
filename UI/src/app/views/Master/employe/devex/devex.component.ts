import { HttpClient, HttpParams } from '@angular/common/http';
import {  OnInit } from '@angular/core';
import { NgModule, Component, enableProdMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import DevExpress from 'devextreme';
import { DxDataGridModule } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import CustomStore from 'devextreme/data/custom_store';
import { Service,Employee } from './DVXSERVICE.service';


@Component({
  selector: 'app-devex',
  templateUrl: './devex.component.html',
  styleUrls: ['./devex.component.scss'],
  providers: [Service],
})
export class DevexComponent implements OnInit {

  customersData: any;

  shippersData: any;

  dataSource: any;

  url: string;

  masterDetailDataSource: any;

  constructor(httpClient: HttpClient) {
    function isNotEmpty(value: any): boolean {
      return value !== undefined && value !== null && value !== '';
    }
    this.dataSource = new CustomStore({
      key: 'id',
      load(loadOptions: any) {
        let params: HttpParams = new HttpParams();
        [
          'skip',
          'take',
          'requireTotalCount',
          'requireGroupCount',
          'sort',
          'filter',
          'totalSummary',
          'group',
          'groupSummary',
        ].forEach((i) => {
          if (i in loadOptions && isNotEmpty(loadOptions[i])) { params = params.set(i, JSON.stringify(loadOptions[i])); }
        });
        return httpClient.post('https://localhost:7087/api/Employ/DevGetEmploys',params)
        .toPromise()
        .then(response => {
            return response;
        })
        .catch(() => { throw 'Data loading error' });
      },
    });

    // this.dataSource = AspNetData.createStore({
    //   key: 'OrderID',
    //   loadUrl: `${this.url}/Orders`,
    //   insertUrl: `${this.url}/InsertOrder`,
    //   updateUrl: `${this.url}/UpdateOrder`,
    //   deleteUrl: `${this.url}/DeleteOrder`,
    //   onBeforeSend(method, ajaxOptions) {
    //     ajaxOptions.xhrFields = { withCredentials: true };
    //   },
    // });

    // this.customersData = AspNetData.createStore({
    //   key: 'Value',
    //   loadUrl: `${this.url}/CustomersLookup`,
    //   onBeforeSend(method, ajaxOptions) {
    //     ajaxOptions.xhrFields = { withCredentials: true };
    //   },
    // });

    // this.shippersData = AspNetData.createStore({
    //   key: 'Value',
    //   loadUrl: `${this.url}/ShippersLookup`,
    //   onBeforeSend(method, ajaxOptions) {
    //     ajaxOptions.xhrFields = { withCredentials: true };
    //   },
    // });
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
 
  

