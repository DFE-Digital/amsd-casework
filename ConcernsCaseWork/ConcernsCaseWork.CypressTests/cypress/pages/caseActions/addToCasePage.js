class AddToCasePage {

    constructor() {
        this.actionOptions = ["DfE support", "Financial forecast", "Financial plan",
                            "Financial returns", "Financial support", "Forced termination",
                            "Notice To Improve (NTI)", "Recovery plan", "School Resource Management Adviser (SRMA)"];

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


    //current status


    //Option accepts the following args: DfESupport | FinancialForecast | FinancialPlan | FinancialReturns |
    //FinancialSupport| ForcedTermination | Nti| RecoveryPlan | Srma | Tff |
    getCaseActionRadio(option) {
        return     cy.get('[value="'+option+'"]');
    }    


    //Methods

    addToCase(option) {

                //cy.get('[class="govuk-heading-l"]').should('contain.text', 'Add to case');
                this.getHeadingText().should('contain.text', 'Add to case');

                this.getSubHeadingText().should('contain.text', 'What action are you taking?');

            this.getCaseActionRadio(option).click();

            //this.getCaseActionRadio(option).siblings().should('contain.text', textToVerify);
        }
        
    }
    
    export default new AddToCasePage();