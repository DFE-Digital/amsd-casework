class FinancialPlanPage {



    constructor() {
        //this.something = 
        //this.arrDate = ["day1", "month1", "year1","day2", "month2", "year2", ];
        this.date = new Date();
        //this.concatDate = "";
        this.arrDate = ["day1", "month1", "year1","day2", "month2", "year2", ];
        this.concatDate =  "setAsNull";

        //this.statusSelect2 = () => {
            //let rand = Math.floor(Math.random()*2)
            //this.getStatusRadio().eq(rand).click();
            //cy.log(this.getStatusRadioLabel().eq(rand).invoke('text'));
            //return this.getStatusRadioLabel().eq(rand).invoke('text');
       // }
    }




    //locators
    getHeadingText() {
        return     cy.get('h1[class="govuk-heading-l"]');
    }

    getSubHeadingText() {
        return     cy.get('h2[class="govuk-heading-m"]');
    }

    getCancelBtn() {
        return     cy.get('[id="cancel-link"]', { timeout: 30000 });
    }

    getAddToCaseBtn() {
        return     cy.get('[data-prevent-double-click="true"]', { timeout: 30000 }).contains('Add to case');
    }


    //Current Status
    getStatusRadio() {
        return     cy.get('[id*="status"]', { timeout: 30000 });
    }

    getStatusRadioLabel() {
        return     cy.get('label.govuk-label.govuk-radios__label', { timeout: 30000 });
    }

    //Date plan requested
    getDatePlanRequestedDay() {
        return     cy.get('[id="dtr-day-plan-requested"]', { timeout: 30000 });
    }

    getDatePlanRequestedMon() {
        return     cy.get('[id="dtr-month-plan-requested"]', { timeout: 30000 });
    }

    getDatePlanRequestedYear() {
        return     cy.get('[id="dtr-year-plan-requested"]', { timeout: 30000 });
    }



    //Date viable plan received
    getDatePlanReceivedDay() {
        return     cy.get('[id="dtr-day-viable-plan"]', { timeout: 30000 });
    }

    getDatePlanReceivedMon() {
        return     cy.get('[id="dtr-month-viable-plan"]', { timeout: 30000 });
    }

    getDatePlanReceivedYear() {
        return     cy.get('[id="dtr-year-viable-plan"]', { timeout: 30000 });
    }

    getNotesBox() {
        return    cy.get('[id="financial-plan-notes"]', { timeout: 30000 });
    }
    
    getNotesInfo() {
        return    cy.get('[id="financial-plan-notes-info"]', { timeout: 30000 });
    }

    getUpdateBtn() {
        return    cy.get('[id="add-financial-plan-button"]', { timeout: 30000 });
    }

    getItemsTable() {
        return    cy.get('[class="govuk-table__row"]', { timeout: 30000 });
    }

    
    //Option accepts the following args: DfESupport | FinancialForecast | FinancialPlan | FinancialReturns |
    //FinancialSupport| ForcedTermination | Nti| RecoveryPlan | Srma | Tff |
    getCaseActionRadio(option) {
        return     cy.get('[value='+option+']');
    }   
    

    //Methods

    statusSelect() {
		let rand = Math.floor(Math.random()*2)

        this.getStatusRadio().eq(rand).click();
        cy.log(this.getStatusRadioLabel().eq(rand).invoke('text'));

        return this.getStatusRadioLabel().eq(rand).invoke('text');
	}




    setDatePlanRequested() {
        //let concatDate = "";
       // let arrDate = ["day1", "month1", "year1","day2", "month2", "year2", ];
       // let concatDate =  "setAsNull";

		this.getDatePlanRequestedDay().type(Math.floor(Math.random() * 21) + 10).invoke('val').then((dtrday) => {
			cy.wrap(dtrday.trim()).as("day")
 this.arrDate[0] = dtrday;
            //arrDate[0] = self.day;
            //arrDate[0] = this.day;
            //cy.log("ins reqday1 wrap as day"+day);
            //cy.log("ins reqday1a wrap as self.day ="+self.day);
            //cy.log("ins reqday1a wrap as self.day ="+self.day);
            cy.log("ins reqday2 ="+dtrday);
            cy.log("ins reqday3 dtrday ="+dtrday);
            //cy.log("ins reqday4 arrDate[0] ="+arrDate[0]);
            //cy.log("reqday "+this.dtrday);
            cy.log("reqday4 "+this.arrDate[0]);

		});
       // cy.log("outs reqday1 wrap as day"+day);
        cy.log("outs reqday1a wrap as self.day ="+self.day);
        //cy.log("outs reqday2 "+dtrday);
        cy.log("outs reqday3 dtrday ="+self.dtrday);
        cy.log("outs reqday4 this.arrDate[0] ="+this.arrDate[0]);

 //this.arrDate[0] = self.dtrday;

		this.getDatePlanRequestedMon().type(Math.floor(Math.random() *3) + 10).invoke('val').then((dtrmon) => {
			cy.wrap(dtrmon.trim()).as("month");
            this.month
            this.arrDate[1] = this.dtrmon;
            //arrDate[1] = this.month;
		});
        this.arrDate[1] = self.dtrmon;

		this.getDatePlanRequestedYear().type("2022").invoke('val').then((dtryr) => {
			cy.wrap(dtryr.trim()).as("year");
           this.arrDate[2] = this.dtryr;
        });
        this.arrDate[2] = self.dtryr;



           //arrDate[2] = this.year;
           cy.log("1 "+self.dtrday+"-"+self.month+"-"+self.year);
           // this.concatDate = (this.day+"-"+this.month+"-"+this.year);
           //this.concatDate = (self.dtrday+"-"+self.month+"-"+self.year);
            this.concatDate = (this.arrDate[0]+"-"+this.arrDate[1]+"-"+this.arrDate[2]);

            this.concatDate = (this.arrDate[0]);
            cy.log("2"+"method concatDate "+this.concatDate);
            //cy.log("3 method concatDate "+concatDate);
            cy.log("method "+self.concatDate);
  //          cy.log("method "+dtrday);
            cy.log("method "+this.dtrday);
            cy.log("method "+self.dtrday);
            //cy.log("method "+arrDate[0]);
            cy.log("method "+this.arrDate[0]);

        return cy.wrap(this.concatDate = (this.arrDate[0]+"-"+this.arrDate[1]+"-"+this.arrDate[2]) )
       // return cy.wrap(this.concatDate = (arrDate[0]+"-"+arrDate[1]+"-"+arrDate[2]) )
       // return cy.wrap(this.concatDate);
       // return cy.wrap(concatDate);
        //return cy.wrap(arrDate[0]);

	}

        /*

    setDatePlanRequestedWORKINGEXAMPLE() {

		this.getDatePlanRequestedDay().type(Math.floor(Math.random() * 21) + 10).invoke('val').then((dtrday) => {
			cy.wrap(dtrday.trim()).as("day")
            this.arrDate[0] = dtrday;

            cy.log("ins reqday2 ="+dtrday);
            cy.log("ins reqday3 dtrday ="+dtrday);
            cy.log("reqday4 "+this.arrDate[0]);

		});

        cy.log("outs reqday4 this.arrDate[0] ="+this.arrDate[0]);


		this.getDatePlanRequestedMon().type(Math.floor(Math.random() *3) + 10).invoke('val').then((dtrmon) => {
			cy.wrap(dtrmon.trim()).as("month");
            //this.month
            this.arrDate[1] = this.dtrmon;

		});
        this.arrDate[1] = self.dtrmon;

		this.getDatePlanRequestedYear().type("2022").invoke('val').then((dtryr) => {
			cy.wrap(dtryr.trim()).as("year");
           this.arrDate[2] = this.dtryr;
        });
        this.arrDate[2] = self.dtryr;

            this.concatDate = (this.arrDate[0]+"-"+this.arrDate[1]+"-"+this.arrDate[2]);
            this.concatDate = (this.arrDate[0]);
            cy.log("2"+"method concatDate "+this.concatDate);
            cy.log("method "+this.arrDate[0]);

        return cy.wrap(this.concatDate = (this.arrDate[0]+"-"+this.arrDate[1]+"-"+this.arrDate[2]) )
	}
    */

    
    /*

    setDatePlanReceived() {

        this.getDatePlanReceivedDay().type(Math.floor(Math.random() * 21) + 10).invoke('val').then(dtrday => {
			cy.wrap(dtrday.trim()).as("day");
		});

		this.getDatePlanReceivedMon().type(Math.floor(Math.random() *3) + 10).invoke('val').then(dtrmon => {
			cy.wrap(dtrmon.trim()).as("month");
        });

		this.getDatePlanReceivedYear().type("2022").invoke('val').then(dtryr => {
			cy.wrap(dtryr.trim()).as("year");
		});

        cy.log(this.day+"-"+this.month+"-"+this.year);	
		this.concatDate = (this.day+"-"+this.month+"-"+this.year);
		cy.log(this.concatDate);

        return this.concatDate
	}
    */
        
}

    export default new FinancialPlanPage();