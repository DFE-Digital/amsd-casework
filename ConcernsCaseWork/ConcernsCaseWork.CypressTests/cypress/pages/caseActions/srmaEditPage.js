class SRMAEditPage {


    constructor() {
        //this.something = 
        this.arrDate =[];
    }


    //locators
    //SRMAAddPage.getDateOfferedDay();

    getHeadingText() {
        return     cy.get('h1[class="govuk-heading-l"]');
    }

    getSubHeadingText() {
        return     cy.get('h2[class="govuk-heading-m"]');
    }

    //SRMA TABLE
    //
    getSrmaTable() {
        return    cy.get('[class="govuk-table__cell"]', { timeout: 30000 });
    }

    getSrmaTableRow() {
        return    cy.get('tr.govuk-table__row', { timeout: 30000 });
    }

    getSrmaTableRowEmpty() {
       // return    cy.get('.govuk-tag.ragtag.ragtag__grey', { timeout: 30000 });
        return    cy.get('tr.govuk-table__row', { timeout: 30000 }).contains('Empty');
    }

    getNotesInfo() {
        return    cy.get('[id="srma-notes-info"]', { timeout: 30000 });
    }

    getTableAddEditLink() {
        return    cy.get('[class="govuk-link"]', { timeout: 30000 });
    }

    //Option accepts the following args: DfESupport | FinancialForecast | FinancialPlan | FinancialReturns |
    //FinancialSupport| ForcedTermination | Nti| RecoveryPlan | Srma | Tff |
    getCaseActionRadio(option) {
        return     cy.get('[value="'+option+'"]');
    }   
    
    getNotesBox() {
        return    cy.get('[id="srma-notes"]', { timeout: 30000 });
    }
    
    getNotesInfo() {
        return    cy.get('[id="srma-notes-info"]', { timeout: 30000 });
    }


    //Methods

    checkForTableEntry() {

        //let $elem = Cypress.$('.govuk-list.govuk-error-summary__list');
        let $elem = Cypress.$('.govuk-tag.ragtag.ragtag__grey');
        cy.log(($elem).length)
        
        return ($elem.length);
    }

    getDateOffered() {

        cy.log("getDateOffered").then(() => {

                  this.getDateOfferedDay().invoke('val').then((day) => {

                       this.getDateOfferedMonth().invoke('val').then((month) => {

                           this.getDateOfferedYear().invoke('val').then((year) => {

                               cy.wrap(day+"-"+month+"-"+year).as("concat").then((concat) => {
   
                               return concat ;
                               });
                           });
                       });

                   });
           });

   }
        
}

    export default new SRMAEditPage();

