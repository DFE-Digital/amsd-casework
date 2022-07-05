class FinancialPlanPage {



    constructor() {
        this.something ="";
//        this.concatDate3 = "hello";
//        this.arrDate = [] //= ["day1", "month1", "year1","day2", "month2", "year2", ];
//        this.concatDate = "setAsNull";
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

    getAddCaseAcrionsBtn() {
        return caseActionsBase.getAddCaseActionBtn();

    }

    
    //Option accepts the following args: DfESupport | FinancialForecast | FinancialPlan | FinancialReturns |
    //FinancialSupport| ForcedTermination | Nti| RecoveryPlan | Srma | Tff |
    getCaseActionRadio(option) {
        return     cy.get('[value='+option+']');
    }   


    

    //Methods


    //sets the Case Action status
    //Takes a string value of either "0", "1", "2" or "random"
    setStatusSelect(value) {
        //let random = false
        cy.log("value "+value)

        if(value == "random"){
            let rand = Math.floor(Math.random()*2)

            this.getStatusRadio().eq(rand).click();
            cy.log(this.getStatusRadioLabel().eq(rand).invoke('text'));
            return this.getStatusRadioLabel().eq(rand).invoke('text');

        }else{
            
            this.getStatusRadio().eq(value).click();
            cy.log(this.getStatusRadioLabel().eq(value).invoke('text'));
            return this.getStatusRadioLabel().eq(value).invoke('text');

        }

	}

    statusSelect() {
		let rand = Math.floor(Math.random()*2)

        this.getStatusRadio().eq(rand).click();
        cy.log(this.getStatusRadioLabel().eq(rand).invoke('text'));

        return this.getStatusRadioLabel().eq(rand).invoke('text');
	}



        //Returns the value set
        setDatePlanRequestedDay() {
            cy.log("setDatePlanRequestedDay")//.then(() => {
                     return this.getDatePlanRequestedDay().type(Math.floor(Math.random() * 21) + 10).invoke('val');
           //});
       }

       setDatePlanRequestedMon() {
            cy.log("setDatePlanRequestedMonth")//.then(() => {
                 return this.getDatePlanRequestedMon().type(Math.floor(Math.random() *3) + 10).invoke('val');
       //});
        }

        setDatePlanRequestedYear() {
            cy.log("setDatePlanRequestedYear")//.then(() => {
                 return this.getDatePlanRequestedYear().type("2023").invoke('val');
       //});
        }

        setDatePlanRequested() {

             cy.log("setDatePlanRequested").then(() => {
    
                       this.getDatePlanRequestedDay().type(Math.floor(Math.random() * 21) + 10).invoke('val').then((day) => {
    
                            this.getDatePlanRequestedMon().type(Math.floor(Math.random() *3) + 10).invoke('val').then((month) => {
    
                                this.getDatePlanRequestedYear().type("2023").invoke('val').then((year) => {

                                    cy.wrap(day+"-"+month+"-"+year).as("concat").then((concat) => {
        
                                    return concat ;
                                    });
                                });
                            });
    
                        });
                });

        }

        setDatePlanReceived() {

            cy.log("setDatePlanReceived").then(() => {
   
                      this.getDatePlanReceivedDay().type(Math.floor(Math.random() * 21) + 10).invoke('val').then((day) => {
   
                           this.getDatePlanReceivedMon().type(Math.floor(Math.random() *3) + 10).invoke('val').then((month) => {
   
                               this.getDatePlanReceivedYear().type("2023").invoke('val').then((year) => {

                                   cy.wrap(day+"-"+month+"-"+year).as("concat").then((concat) => {
       
                                   return concat ;
                                   });
                               });
                           });
   
                       });
               });

       }



        
}

    export default new FinancialPlanPage();