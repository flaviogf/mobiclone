import 'package:bloc/bloc.dart';
import 'package:mobiclone/core/sign_up/name_repository.dart';
import 'package:mobiclone/pages/name/name_event.dart';
import 'package:mobiclone/pages/name/name_state.dart';

class NameBloc extends Bloc<NameEvent, NameState> {
  final NameRepository _nameRepository;

  NameBloc(this._nameRepository);

  @override
  NameState get initialState => InitiatedNameState();

  @override
  Stream<NameState> mapEventToState(NameEvent event) async* {
    if (event is NameEventSubmit) {
      yield SubmittedNameState(event.value);

      if (event.value.isEmpty) {
        yield RequiredNameState(event.value, 'The name must be informed.');
      } else {
        final String name = await _nameRepository.addName(event.value);
        yield ValidatedNameState(name);
      }
    }
  }
}
