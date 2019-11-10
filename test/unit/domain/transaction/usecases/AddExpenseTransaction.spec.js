import Factory from '../../../../Factory'
import AddExpenseTransaction from '../../../../../src/domain/transaction/usecases/AddExpenseTransaction'

describe('AddExpenseTransaction', () => {
  let user = null
  let account = null
  let useCase = null

  beforeEach(async () => {
    user = await Factory.user()

    account = await Factory.account()

    const userRepository = {
      findById: jest.fn().mockResolvedValue(user),
      persist: jest.fn().mockResolvedValue(user),
    }

    const accountRepository = {
      findById: jest.fn().mockResolvedValue(account),
      persist: jest.fn().mockResolvedValue(account),
    }

    useCase = new AddExpenseTransaction({ userRepository, accountRepository })
  })

  it('should add expense to transaction list', async () => {
    await useCase.execute({
      userId: await user.id(),
      accountId: await account.id(),
      value: 1000,
    })

    expect(await account.transactions()).toHaveLength(1)
  })

  it('should add point to point list', async () => {
    await useCase.execute({
      userId: await user.id(),
      accountId: await account.id(),
      value: 1000,
    })

    expect(await user.points()).toHaveLength(1)
  })
})
