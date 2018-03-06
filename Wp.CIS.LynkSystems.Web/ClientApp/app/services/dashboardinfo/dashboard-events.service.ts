
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { TerminalEquipment } from '../../models/dashboardinfo/terminalequipment.model';

Injectable()
export class DashboardEventsService {

    // Observable string sources
    private terminalEquipmentChangedSource = new Subject<TerminalEquipment>();

    // Observable string streams
    public terminalEquipmentChangeList$ = this.terminalEquipmentChangedSource.asObservable();

    constructor() {

    }

    // Service message commands
    public announceTerminalEquipmentChange(terminalEquip: TerminalEquipment) {

        this.terminalEquipmentChangedSource.next(terminalEquip);

    }


}