import { startOfMinute } from 'date-fns'

import Factory from '../../../../Factory'
import Expense from '../../../../../src/domain/transaction/entities/Expense'
import Revenue from '../../../../../src/domain/transaction/entities/Revenue'

describe('Account', () => {
  describe('addRevenue', () => {
    it('should be balance to equal 1000 cents', async () => {
      const account = await Factory.account()

      const value = 1000

      await account.addRevenue({ value })

      expect(await account.balance()).toEqual(1000)
    })

    it('should add revenue to transaction list', async () => {
      const account = await Factory.account()

      const value = 1000

      await account.addRevenue({ value })

      expect(await account.transactions()).toHaveLength(1)
    })

    it('should return added revenue', async () => {
      const account = await Factory.account()

      const value = 1000

      const revenue = await account.addRevenue({ value })

      expect(revenue).toBeInstanceOf(Revenue)
      expect(await revenue.value()).toEqual(value)
      expect(await revenue.date()).toEqual(startOfMinute(new Date()))
    })
  })

  describe('addExpense', () => {
    it('should be balance to equal -1000', async () => {
      const account = await Factory.account()

      const value = 1000

      await account.addExpense({ value })

      expect(await account.balance()).toEqual(-value)
    })

    it('should add expense to transaction list', async () => {
      const account = await Factory.account()

      const value = 1000

      await account.addExpense({ value })

      expect(await account.transactions()).toHaveLength(1)
    })

    it('should return added expense', async () => {
      const account = await Factory.account()

      const value = 1000

      const expense = await account.addExpense({ value })

      expect(expense).toBeInstanceOf(Expense)
      expect(await expense.value()).toEqual(value)
      expect(await expense.date()).toEqual(startOfMinute(new Date()))
    })
  })
})
