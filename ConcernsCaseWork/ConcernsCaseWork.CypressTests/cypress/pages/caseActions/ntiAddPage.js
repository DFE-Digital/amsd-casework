class NTIAddPage {

    constructor() {
        //this.something = 
        this.arrDate = ["day1", "month1", "year1","day2", "month2", "year2", ];
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


    //Status
    getStatusRadio() {
        return     cy.get('[id*="status"]', { timeout: 30000 });
    }

    getStatusRadioLabel() {
        return     cy.get('label.govuk-label.govuk-radios__label', { timeout: 30000 });
    }

    //Date Offered
    getDateOfferedDay() {
        return     cy.get('[id="dtr-day"]', { timeout: 30000 });
    }

    getDateOfferedMonth() {
        return     cy.get('[id="dtr-month"]', { timeout: 30000 });
    }

    getDateOfferedYear() {
        return     cy.get('[id="dtr-year"]', { timeout: 30000 });
    }

    getNotesTextBox() {
        return     cy.get('[id="financial-plan-notes"]', { timeout: 30000 });
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

    statusSelect() {
		let rand = Math.floor(Math.random()*2)

        this.getStatusRadio().eq(rand).click();
        cy.log(this.getStatusRadioLabel().eq(rand).invoke('text'));
        return this.getStatusRadioLabel().eq(rand).invoke('text');
	}
        
}

    export default new NTIAddPage();
