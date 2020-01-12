import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/pages/name/name_bloc.dart';
import 'package:mobiclone/pages/name/name_event.dart';
import 'package:mobiclone/pages/name/name_state.dart';

void main() {
  group('NameBloc should', () {
    NameBloc _bloc;

    setUp(() {
      _bloc = NameBloc();
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
      'return "ValidatedNameState" when the name submitted was valid',
      () {
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
