class CaseActionsBasePage {

    constructor() {
        //this.something = 
        this.arrDate = [];
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

    getContinueBtn() {
        return     cy.get('[data-prevent-double-click="true"]', { timeout: 30000 }).contains('Add to case');
    }

    getAddCaseActionBtn() {
        return     cy.get('[id="add-srma-button"]', { timeout: 30000 });
    }


    //Methods


    setDateFIXME() {

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
        
}

    export default new CaseActionsBasePage();
