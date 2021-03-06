
describe('Footer links direct to the correct pages from Active Casework page', () => {
    before(() => {
		cy.login();
	});

	afterEach(() => {
		cy.storeSessionData();
	});

    it('Should open Accessibility link', () => {
        cy.wait(500);
        cy.fixture('footer-links-fixture').then((footerLinkData) =>{
        cy.get('.govuk-footer__link[href="'+footerLinkData.accessibilityLink+'"]', { timeout: 50000 }).click()
        cy.get('.govuk-grid-column-two-thirds-from-desktop h1').contains(footerLinkData.accessibilityTestText)
        })
        cy.get('#back-link-event').click()
    });

    it('Should open Cookies link', () => {
        cy.wait(500);
        cy.fixture('footer-links-fixture').then((footerLinkData) =>{
        cy.get('.govuk-footer__link[href="'+footerLinkData.cookiesLink+'"]', { timeout: 50000 }).click()
        cy.get('.govuk-grid-column-two-thirds-from-desktop h1').contains(footerLinkData.cookiesTestText)
        })
        cy.get('#back-link-event').click()
    });

    it('Should open Privacy Policy link', () => {
        cy.wait(500);
        cy.fixture('footer-links-fixture').then((footerLinkData) =>{
        cy.get('.govuk-footer__link[href="'+footerLinkData.privacyPolicyLink+'"]', { timeout: 50000 }).click()
        cy.get('.govuk-grid-column-two-thirds-from-desktop h2').contains(footerLinkData.privacyPolicyText)
        })
        cy.get('#back-link-event').click()
    });

    it('Should open Admin link and clicking the "Back To Casework" button should take the user back to the dashboard ', () => {
        cy.wait(500);
        cy.fixture('footer-links-fixture').then((footerLinkData) =>{
        cy.get('.govuk-footer__link[href="'+footerLinkData.adminLink+'"]', { timeout: 50000 }).click()
        cy.get('.govuk-heading-l').contains(footerLinkData.adminText)
        })
        cy.get('.govuk-button--secondary[href="/"]').click()
        cy.get('.govuk-button[href="/case"]').should('be.visible')
    });

    after(function () {
        cy.clearLocalStorage();
        cy.clearCookies()
    });
});