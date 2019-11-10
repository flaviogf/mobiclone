import Factory from '../../../../Factory'
import Point from '../../../../../src/domain/transaction/entities/Point'

describe('User', () => {
  describe('addPoint', () => {
    it('should add point to point list', async () => {
      const user = await Factory.user()

      const description = 'first revenue of day'

      const value = 1000

      await user.addPoint({ description, value })

      expect(await user.points()).toHaveLength(1)
    })

    it('should increment points by value', async () => {
      const user = await Factory.user()

      const description = 'first revenue of day'

      const value = 1000

      await user.addPoint({ description, value })

      expect(await user.totalOfPoints()).toEqual(value)
    })

    it('should return added point', async () => {
      const user = await Factory.user()

      const description = 'first revenue of day'

      const value = 1000

      const point = await user.addPoint({ description, value })

      expect(point).toBeInstanceOf(Point)
      expect(await point.description()).toEqual(description)
      expect(await point.value()).toEqual(value)
    })
  })
})
