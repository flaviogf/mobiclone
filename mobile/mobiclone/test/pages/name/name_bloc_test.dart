import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/core/sign_up/name_repository.dart';
import 'package:mobiclone/pages/name/name_bloc.dart';
import 'package:mobiclone/pages/name/name_event.dart';
import 'package:mobiclone/pages/name/name_state.dart';
import 'package:mockito/mockito.dart';

class MockNameRepository extends Mock implements NameRepository {}

void main() {
  group('NameBloc should', () {
    MockNameRepository _nameRepository;
    NameBloc _bloc;

    setUp(() {
      _nameRepository = MockNameRepository();
      _bloc = NameBloc(_nameRepository);
    });

    tearDown(() {
      _bloc.close();
    });

    test(
      'have the initial state equal to "InitiatedNameState" with empty value',
      () {
        expect(_bloc.initialState, InitiatedNameState());
      },
    );

    test(
      'return "ValidatedNameState" when the name submitted is valid',
      () {
        when(
          _nameRepository.addName('flavio'),
        ).thenAnswer(
          (_) => Future.value('flavio'),
        );

        _bloc.add(NameEventSubmit('flavio'));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedNameState(),
            SubmittedNameState('flavio'),
            ValidatedNameState('flavio'),
          ]),
        );
      },
    );

    test(
      'return "RequiredNameState" when the name submitted is empty',
      () {
        _bloc.add(NameEventSubmit(''));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedNameState(),
            SubmittedNameState(''),
            RequiredNameState('', 'The name must be informed.'),
          ]),
        );
      },
    );
  });
}
