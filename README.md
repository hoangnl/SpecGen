# SpecGen
Converts textual scenarios to code

## Proof of Concept
To PoC aims to take a story in the format:

```
Account holder withdraws cash
As an Account Holder
I want to withdraw cash from an ATM
So that I can get money when the bank is closed

Account has sufficient fund
Given the account balance is $100
And the Card is valid
And the machine contains enough money
When the account holder requests $20
Then the ATM should dispense $20
And the account balance should be $80
And the card should be returned

Card has been disabled
Given the Card is disabled
When the Account Holder requests 20
Then Card is retained
And the ATM should say the Card has been retained

Account has insufficient funds
Given the Account Balance is $10
And the Card is valid
And the machine contains enough money
When the Account Holder requests $20
Then the ATM should not dispense any Money
And the ATM should say there are Insufficient Funds
And the Account Balance should be $20
And the Card should be returned
```

and render it in the format:

```csharp
using System;
using BddfySpecGen.ATM;
using Specify.Stories;
using TestStack.BDDfy;

namespace BddfySpecGen
{
    public class AccountHolderWithdrawsCash : UserStory
    {
        public AccountHolderWithdrawsCash()
        {
            AsA = "As an Account Holder";
            IWant = "I want to withdraw cash from an ATM";
            SoThat = "So that I can get money when the bank is closed";
        }
    }
    public class AccountHasSufficientFund : ScenarioFor<object,  AccountHolderWithdrawsCash>
    {
        public void GivenTheAccountBalanceIsDollars100()
        {
            throw new NotImplementedException();
        }
        [("And the Card is valid")]
        public void AndTheCardIsValid()
        {
            throw new NotImplementedException();
        }
        [("And the machine contains enough money")]
        public void AndTheMachineContainsEnoughMoney()
        {
            throw new NotImplementedException();
        }
        public void WhenTheAccountHolderRequestsDollars20()
        {
            throw new NotImplementedException();
        }
        public void ThenTheATMShouldDispenseDollars20()
        {
            throw new NotImplementedException();
        }
        [("And the account balance should be $80")]
        public void AndTheAccountBalanceShouldBeDollars80()
        {
            throw new NotImplementedException();
        }
        [("And the card should be returned")]
        public void AndTheCardShouldBeReturned()
        {
            throw new NotImplementedException();
        }
    }

    public class CardHasBeenDisabled : ScenarioFor<object,  AccountHolderWithdrawsCash>
    {
        public void GivenTheCardIsDisabled()
        {
            throw new NotImplementedException();
        }
        public void WhenTheAccountHolderRequests20()
        {
            throw new NotImplementedException();
        }
        public void ThenCardIsRetained()
        {
            throw new NotImplementedException();
        }
        [("And the ATM should say the Card has been retained")]
        public void AndTheATMShouldSayTheCardHasBeenRetained()
        {
            throw new NotImplementedException();
        }
    }

    public class AccountHasInsufficientFunds : ScenarioFor<object,  AccountHolderWithdrawsCash>
    {
        public void GivenTheAccountBalanceIsDollars10()
        {
            throw new NotImplementedException();
        }
        [("And the Card is valid")]
        public void AndTheCardIsValid()
        {
            throw new NotImplementedException();
        }
        [("And the machine contains enough money")]
        public void AndTheMachineContainsEnoughMoney()
        {
            throw new NotImplementedException();
        }
        public void WhenTheAccountHolderRequestsDollars20()
        {
            throw new NotImplementedException();
        }
        public void ThenTheATMShouldNotDispenseAnyMoney()
        {
            throw new NotImplementedException();
        }
        [("And the ATM should say there are Insufficient Funds")]
        public void AndTheATMShouldSayThereAreInsufficientFunds()
        {
            throw new NotImplementedException();
        }
        [("And the Account Balance should be $20")]
        public void AndTheAccountBalanceShouldBeDollars20()
        {
            throw new NotImplementedException();
        }
        [("And the Card should be returned")]
        public void AndTheCardShouldBeReturned()
        {
            throw new NotImplementedException();
        }
    }

}
```
