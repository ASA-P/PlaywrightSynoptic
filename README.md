# **Automated Testing with Playwright**
### **This guide introduces how to best develop resilient and fast automated tests with Playwright and is split up into five parts:**
- ### [**Useful information**](https://github.com/ASA-P/PlaywrightSynoptic#useful-information-1)
- ### [**Setting Up and Using Playwright on Local Development Environment**](https://github.com/ASA-P/PlaywrightSynoptic#setting-up-and-using-playwright-on-local-development-environment-1)
- ### [**How to Save Authentication in Playwright**](https://github.com/ASA-P/PlaywrightSynoptic#how-to-save-authentication-in-playwright-1)
- ### [**How to Set Up a CirrusInsite Page with Authentication**](https://github.com/ASA-P/PlaywrightSynoptic#how-to-set-up-cirrusinsite-page-with-authentication)
- ### [**Visual Testing**](https://github.com/ASA-P/PlaywrightSynoptic#visual-testing-1)

# **Table of Contents**
## **Useful information**
  - ### [Useful Links](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#useful-links)
    - [Playwright .NET website](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#playwright-net-website-httpsplaywrightdevdotnet-)
    - [Playwright .NET docs](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#playwright-net-docs-httpsplaywrightdevdotnetdocsintro-)
    - [Guides](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#guides-click-the-top-left-button-to-view-guides-on-httpsplaywrightdevdotnetdocsintro-)
    - [Playwright .NET Application Programming Interface](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#playwright-net-application-programming-interface-httpsplaywrightdevdotnetdocsapiclass-playwright-)
    - [Online Demo of Playwright](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#online-demo-of-playwright)
      - [Demos](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#demos)
      - [Page screenshot](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#page-screenshot-this-code-snippet-navigates-to-the-playwright-github-repository-in-webkit-and-saves-a-screenshot)
      - [Generate PDF](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#generate-pdf-this-code-snippet-navigates-to-the-playwright-github-repository-and-generates-a-pdf-file-and-saves-it-to-disk)
      - [Request and response logging](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#request-and-response-logging-this-example-will-navigate-to-examplecom-and-log-all-its-request-methods-and-urls-and-for-the-response-the-status)
      - [Device emulation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#device-emulation-this-example-emulates-a-pixel-2-and-creates-a-screenshot-with-its-screen-size)
  - ### [Playwright Key Concepts](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#playwright-key-concepts)
      - [Playwright](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#playwright)
        - [Playwright Class Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#playwright-class-documentation)
        - [Example](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#playwright-module-provides-a-method-to-launch-a-browser-instance-the-following-is-a-typical-example-of-using-playwright-to-drive-automation)
      - [Browsers](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#browsers)
        - [Browsers Documentaion](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#browsers-documentaion)
        - [Browser Class Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#browser-class-documentation)
      - [Browser Contexts](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#browser-contexts)
        - [Browser Contexts Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#browser-contexts-documentation)
        - [Browser context class Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#browser-context-class-documentation)
      - [Pages](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#pages)
        - [Pages Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#pages-documentation)
        - [Page class Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#page-class-documentation)
      - [Page Object Model](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#page-object-model)
        - [Page Object Model Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#page-object-model-documentation)
  - ### [Debugging](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#debugging-1)
    - [Error messages](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#error-messages)
    - [Common Script Errors](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#common-script-errors)
      - [Error - Element not found](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#error---element-not-found)
        - [Possible causes](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#possible-causes)
      - [Error - Click not executed](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#error---click-not-executed)
        - [Possible causes](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#possible-causes-1)
      - [Error - Element not visible](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#error---element-not-visible)
        - [Possible causes](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#possible-causes-2)
  - ### [Debugging Tools](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#debugging-tools-1)
    - [Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#debugging-tools-playwright-documentation-httpsplaywrightdevdocsdebug)
    - [Run in Debug Mode](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#run-in-debug-mode)
    - [The Playwright Inspector](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#the-playwright-inspector)
    - [Run in headed mode](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#run-in-headed-mode)
    - [slowMo](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#slowmo)
    - [Page.PauseAsync()](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#pagepauseasync)
  - ### [Best Practices](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#best-practices-1)
    - [Keeping tests valuable](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#keeping-tests-valuable)
      - [Keep tests short](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#keep-tests-short)
      - [Keep tests focused](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#keep-tests-focused)
    - [Choosing selectors](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#choosing-selectors)
      - [Attributes of a Good Selector](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#the-attributes-of-a-good-selector-are)
      - [Examples of (potentially) good selectors](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#examples-of-potentially-good-selectors)
      - [Examples of bad selectors](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#examples-of-bad-selectors)
  - ### [Playwright Tools](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#playwright-tools-1)
    - [Generate code / selectors](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#generate-code--selectors)
    - [Run tests in debug mode](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Useful%20Information.md#run-tests-in-debug-mode)
## **Setting Up and Using Playwright on Local Development Environment**
  - ### [Setting Up and Using Playwright on Local Development Environment](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#setting-up-and-using-playwright-on-local-development-Environment-1)
      - ### [Prerequisites](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#prerequisites-1)
      - ### [Creating a Playwright Project](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#creating-playwright-project)
      -   ### [Adding Tests](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#adding-tests-1)
          -  [Page options documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#page-options-documentation)
          -  [Assert options documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#assert-options-documentation)
      -  ### [Autogenerate test script](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#autogenerate-test-script-1)
      -  ### [Change Browser Options](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#change-browser-options-1)
      - ### [Change Browser Context & Tracing Options](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#change-browser-context--tracing-options-1)
          - [Context Options Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#context-options-documentation)
          - [Tracing Options Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#tracing-options-documentation)
      -  ### [Run Configuration Environment Variables](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#run-configuration-environment-variables-in-devrunsettings)
          -  [PWDEBUG](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#pwdebug)
          -  [BrowserType.LaunchAsync(options)](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#browsertypelaunchasyncoptions)
              -  [Headed](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#headed)
              -  [Slowmo](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#slowmo)
              -  [Timeout](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#timeout)
              -  [Browser](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#browser)
          -  [Tracing](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#tracing)
              -  [Tracing documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#tracing-documentation)
          -  [Video](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#video)
              -  [Video Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#video-documentation)

## **How to Save Authentication in Playwright**
- ### [How to Save Authentication in Playwright](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#how-to-save-authentication-in-playwright-2)
    - ### [Explanation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#explanation-1)
        - [Authentication Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#authentication-documentation)
        - [Automate logging in](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#automate-logging-in)
        - [Reuse authentication state](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#reuse-authentication-state)
    - ### [Authentication Demo/Sample Code](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#authentication-demosample-code-1)
        - [Context Class](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#context-class)
        - [Page Class](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#page-class)
        - [Login Test](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#login-test)

## **How to Set Up CirrusInsite Page with Authentication**
- ### [Setup Instructions](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/CirrusInsite%20Test%20Setup.md#setup-instructions-1)
    - [Using Authentication from Authentication File](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/CirrusInsite%20Test%20Setup.md#using-authentication-from-authentication-file)
    - [Login Test](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/CirrusInsite%20Test%20Setup.md#login-test)

## **Visual Testing**
- ### [Taking Screenshot with Playwright](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#taking-screenshot-with-playwright-1)
    -  [Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#documentation)
    - [Viewport screenshot](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#viewport-screenshot)
    -  [Full page screenshot](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#full-page-screenshot)
    - [Element screenshot](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#element-screenshot)
    -  [Change options](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#change-options-with)
-  ### [Using Image Comparison](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#using-image-comparison-1)
    -  [Install](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#install-the-codeuctivityimagesharpcompare-nuget-package-to-compare-images-for-visual-differences)
    -  [Code Snippet](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#below-code-snippet-demonstrates-how-to-use-playwright-screenshot-functionality-and-image-comparison-tools)
