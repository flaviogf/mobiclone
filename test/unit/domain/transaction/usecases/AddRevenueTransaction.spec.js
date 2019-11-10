import Factory from '../../../../Factory'
import AddRevenueTransaction from '../../../../../src/domain/transaction/usecases/AddRevenueTransaction'

describe('AddRevenueTransaction', () => {
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

    useCase = new AddRevenueTransaction({ userRepository, accountRepository })
  })

  it('should add revenue to transaction list', async () => {
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
